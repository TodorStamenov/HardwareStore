namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Models.Sales;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly HardwareStoreDbContext db;

        public SaleService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public int Total(string search)
        {
            return this.db
                .Sales
                .Filter(search)
                .Count();
        }

        public bool Add(
            Dictionary<int, int> items,
            string firstName,
            string lastName,
            string address,
            string phone,
            int userId)
        {
            if (!this.db.Users.Any(u => u.Id == userId))
            {
                return false;
            }

            Sale sale = new Sale
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Phone = phone,
                UserId = userId,
                Date = DateTime.UtcNow
            };

            foreach (var itemInfo in items)
            {
                Item item = this.db.Items.Find(itemInfo.Key);

                if (item == null)
                {
                    continue;
                }

                int quantity = itemInfo.Value > item.Quantity
                    ? item.Quantity
                    : itemInfo.Value;

                item.Quantity -= quantity;

                sale.Items.Add(new ItemSale
                {
                    ItemId = item.Id,
                    Item = item,
                    Quantity = quantity,
                    Price = item.Price.PriceWithDiscount(item.Discount.GetValueOrDefault()) * quantity
                });
            }

            sale.TotalPrice = sale.Items
                .Sum(i => i.Item.Price.PriceWithDiscount(i.Item.Discount.GetValueOrDefault()) * i.Quantity);

            this.db.Sales.Add(sale);
            this.db.SaveChanges();

            return true;
        }

        public SaleDetailsServiceModel Details(int id)
        {
            return this.db
                .Sales
                .Where(s => s.Id == id)
                .AsEnumerable()
                .Select(s => new SaleDetailsServiceModel
                {
                    Id = s.Id,
                    Username = s.User.UserName,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Address = s.Address,
                    Phone = s.Phone,
                    ProfileImage = s.User.ProfileImage.ConvertImage(),
                    Date = s.Date.ToLocalTime(),
                    ItemsCount = s.Items.Count,
                    TotalItems = s.Items.Sum(i => i.Quantity),
                    TotalPrice = s.TotalPrice,
                    Items = s.Items
                        .Select(i => new ListItemsInSaleServiceModel
                        {
                            Id = i.ItemId,
                            Name = i.Item.Name,
                            Image = i.Item.Image.ConvertImage(),
                            Price = i.Price,
                            Quantity = i.Quantity,
                            SingleItemPrice = (i.Price / i.Quantity).RoundPrice()
                        })
                })
                .FirstOrDefault();
        }

        public IEnumerable<ListSalesServiceModel> All(int page, int pageSize, string search)
        {
            return this.db
                 .Sales
                 .Filter(search)
                 .OrderByDescending(s => s.Date)
                 .ThenByDescending(s => s.TotalPrice)
                 .ThenBy(s => s.User.UserName)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ProjectTo<ListSalesServiceModel>()
                 .ToList();
        }
    }
}