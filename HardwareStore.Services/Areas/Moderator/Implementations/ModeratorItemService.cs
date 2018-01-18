namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using Data;
    using Data.Models;
    using Models.Items;
    using System;
    using System.Linq;

    public class ModeratorItemService : IModeratorItemService
    {
        private readonly HardwareStoreDbContext db;

        public ModeratorItemService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool NameExists(string name)
        {
            return this.db
                .Items
                .Any(i => i.Name == name);
        }

        public string GetName(int id)
        {
            return this.db
                .Items
                .Where(i => i.Id == id)
                .Select(i => i.Name)
                .FirstOrDefault();
        }

        public int Create(
            string name,
            string description,
            decimal price,
            int? discount,
            int quantity,
            int warrantyLength,
            byte[] itemImage,
            int subCategoryId)
        {
            if (this.NameExists(name)
                || !this.db.SubCategories.Any(sc => sc.Id == subCategoryId))
            {
                return -1;
            }

            Item item = new Item
            {
                Name = name,
                Description = description,
                Price = price,
                Discount = discount,
                Quantity = quantity,
                WarrantyLength = warrantyLength,
                Image = itemImage,
                UploadDate = DateTime.UtcNow,
                SubCategoryId = subCategoryId
            };

            this.db.Items.Add(item);
            this.db.SaveChanges();

            return item.Id;
        }

        public bool Edit(
            int id,
            string name,
            string description,
            decimal price,
            int? discount,
            int quantity,
            int warrantyLength,
            byte[] itemImage,
            int subCategoryId)
        {
            Item item = this.db.Items.Find(id);

            if (item == null
                || !this.db.SubCategories.Any(sc => sc.Id == subCategoryId)
                || (this.NameExists(name) && item.Name != name))
            {
                return false;
            }

            item.Name = name;
            item.Description = description;
            item.Price = price;
            item.Discount = discount;
            item.Quantity = quantity;
            item.WarrantyLength = warrantyLength;
            item.SubCategoryId = subCategoryId;

            if (itemImage != null)
            {
                item.Image = itemImage;
            }

            this.db.SaveChanges();

            return true;
        }

        public ItemFormServiceModel GetForm(int id)
        {
            return this.db
                .Items
                .Where(i => i.Id == id)
                .Select(i => new ItemFormServiceModel
                {
                    Name = i.Name,
                    Price = i.Price,
                    Discount = i.Discount,
                    Quantity = i.Quantity,
                    WarrantyLength = i.WarrantyLength,
                    Description = i.Description
                })
                .FirstOrDefault();
        }
    }
}