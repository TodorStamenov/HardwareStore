namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Categories;
    using Models.SubCategories;
    using Services.Models.Categories;
    using System.Collections.Generic;
    using System.Linq;

    public class SubCategoryService : ISubCategoryService
    {
        private readonly HardwareStoreDbContext db;

        public SubCategoryService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int id)
        {
            return this.db
                 .SubCategories
                 .Any(sc => sc.Id == id);
        }

        public bool HasItems(int id)
        {
            return this.db
                .SubCategories
                .Where(sc => sc.Id == id)
                .SelectMany(sc => sc.Items)
                .Any();
        }

        public bool NameExists(string name)
        {
            return this.db
                .SubCategories
                .Any(sc => sc.Name == name);
        }

        public string GetName(int id)
        {
            return this.db
                .SubCategories
                .Where(sc => sc.Id == id)
                .Select(sc => sc.Name)
                .FirstOrDefault();
        }

        public bool Create(int categoryId, string name)
        {
            CategoryInfoServiceModel categoryInfo = this.GetCategoryInfo(categoryId);

            if (this.NameExists(name)
                || categoryInfo == null
                || categoryInfo.IsDeleted)
            {
                return false;
            }

            SubCategory subCategory = new SubCategory
            {
                CategoryId = categoryId,
                Name = name
            };

            this.db.SubCategories.Add(subCategory);
            this.db.SaveChanges();

            return true;
        }

        public bool Edit(int id, int categoryId, string name)
        {
            SubCategory subCategory = this.db.SubCategories.Find(id);

            CategoryInfoServiceModel categoryInfo = this.GetCategoryInfo(categoryId);

            if (subCategory == null
                || categoryInfo == null
                || categoryInfo.IsDeleted
                || (this.NameExists(name)
                    && subCategory.Name != name))
            {
                return false;
            }

            subCategory.CategoryId = categoryId;
            subCategory.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public SubCategoryFormServiceModel GetForm(int id)
        {
            return this.db
                .SubCategories
                .Where(sc => sc.Id == id)
                .ProjectTo<SubCategoryFormServiceModel>()
                .FirstOrDefault();
        }

        public IEnumerable<MenuCategoryServiceModel> GetMenu()
        {
            return this.db
                .SubCategories
                .OrderBy(sc => sc.Name)
                .ProjectTo<MenuCategoryServiceModel>()
                .ToList();
        }

        private CategoryInfoServiceModel GetCategoryInfo(int categoryId)
        {
            return this.db
                .Categories
                .Where(c => c.Id == categoryId)
                .ProjectTo<CategoryInfoServiceModel>()
                .FirstOrDefault();
        }
    }
}