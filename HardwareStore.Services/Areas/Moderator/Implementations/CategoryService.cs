namespace HardwareStore.Services.Areas.Moderator.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models.Categories;
    using Services.Models.Categories;
    using Services.Models.SubCategories;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly HardwareStoreDbContext db;

        public CategoryService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public bool Exists(int id)
        {
            return this.db
                .Categories
                .Any(sc => sc.Id == id);
        }

        public bool HasItems(int id)
        {
            return this.db
                .Categories
                .Where(c => c.Id == id)
                .SelectMany(c => c.SubCategories.SelectMany(sc => sc.Items))
                .Any();
        }

        public bool NameExists(string name)
        {
            return this.db
                .Categories
                .Any(c => c.Name == name);
        }

        public string GetName(int id)
        {
            return this.db
                .Categories
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();
        }

        public bool Create(string name)
        {
            if (this.NameExists(name))
            {
                return false;
            }

            Category category = new Category
            {
                Name = name
            };

            this.db.Categories.Add(category);
            this.db.SaveChanges();

            return true;
        }

        public bool Edit(int id, string name)
        {
            Category category = this.db.Categories.Find(id);

            if (category == null
                || (this.NameExists(name)
                    && category.Name != name))
            {
                return false;
            }

            category.Name = name;

            this.db.SaveChanges();

            return true;
        }

        public CategoryFormServiceModel GetForm(int id)
        {
            return this.db
                .Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryFormServiceModel>()
                .FirstOrDefault();
        }

        public IEnumerable<MenuCategoryServiceModel> GetMenu()
        {
            return this.db
                .Categories
                .Where(c => c.SubCategories.Any(sc => sc.Items.Any()))
                .Select(c => new MenuCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    SubCategories = c.SubCategories
                        .Select(sc => new MenuSubCategoryServiceModel
                        {
                            Id = sc.Id,
                            Name = sc.Name
                        })
                })
                .ToList();
        }

        public IEnumerable<ListCategoriesServiceModel> All()
        {
            return this.db
                .Categories
                .ProjectTo<ListCategoriesServiceModel>()
                .ToList();
        }
    }
}