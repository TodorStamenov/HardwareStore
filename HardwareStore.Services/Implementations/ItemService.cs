namespace HardwareStore.Services.Implementations
{
    using Data;
    using Data.Models;
    using Infrastructure.Extensions;
    using Models.Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ItemService : IItemService
    {
        private readonly HardwareStoreDbContext db;

        public ItemService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public int Total(string search)
        {
            return this.db
                .Items
                .Filter(search)
                .Count();
        }

        public int TotalByCategory(int categoryId)
        {
            return this.db
                .Items
                .Where(i => i.SubCategory.CategoryId == categoryId)
                .Count();
        }

        public int TotalBySubCategory(int subCategoryId)
        {
            return this.db
                 .Items
                 .Where(i => i.SubCategoryId == subCategoryId)
                 .Count();
        }

        public ItemDetailsServiceModel Details(int id)
        {
            Item item = this.db.Items.Find(id);

            if (item == null)
            {
                return null;
            }

            item.Views++;
            this.db.SaveChanges();

            return this.db
                .Items
                .Where(i => i.Id == id)
                .AsEnumerable()
                .Select(i => new ItemDetailsServiceModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Image = i.Image.ConvertImage(),
                    Price = i.Price.RoundPrice(),
                    Discount = i.Discount,
                    PriceWithDiscount = i.Price.PriceWithDiscount(i.Discount.GetValueOrDefault()),
                    Quantity = i.Quantity,
                    WarrantyLength = i.WarrantyLength,
                    UploadDate = i.UploadDate.ToLocalTime(),
                    SubCategoryId = i.SubCategoryId,
                    Rating = Math.Round(
                        i.Reviews.Select(r => (int)r.Mark).DefaultIfEmpty().Sum(r => r) / (double)(i.Reviews.Any() ? i.Reviews.Count : 1),
                        1,
                        MidpointRounding.AwayFromZero)
                })
                .FirstOrDefault();
        }

        public IEnumerable<ListItemsServiceModel> ByCategory(int page, int pageSize, int categoryId)
        {
            return this.db
                .Items
                .Where(i => i.SubCategory.CategoryId == categoryId)
                .OrderByDescending(i => i.UploadDate)
                .ThenBy(i => i.Price)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectToListModel()
                .ToList();
        }

        public IEnumerable<ListItemsServiceModel> BySubCategory(int page, int pageSize, int subCategoryId)
        {
            return this.db
                .Items
                .Where(i => i.SubCategoryId == subCategoryId)
                .OrderByDescending(i => i.UploadDate)
                .ThenBy(i => i.Price)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectToListModel()
                .ToList();
        }

        public IEnumerable<ListItemsServiceModel> All(int page, int pageSize, string search)
        {
            return this.db
                .Items
                .Filter(search)
                .OrderByDescending(i => i.UploadDate)
                .ThenBy(i => i.Price)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectToListModel()
                .ToList();
        }
    }
}