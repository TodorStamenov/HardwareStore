namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Infrastructure.Extensions;
    using Models.Sales;
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