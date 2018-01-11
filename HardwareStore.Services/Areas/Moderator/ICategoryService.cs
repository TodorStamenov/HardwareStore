namespace HardwareStore.Services.Areas.Moderator
{
    using Models.Categories;
    using Services.Models.Categories;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        bool Exists(int id);

        bool HasItems(int id);

        bool NameExists(string name);

        string GetName(int id);

        bool Create(string name);

        bool Edit(int id, string name);

        CategoryFormServiceModel GetForm(int id);

        IEnumerable<MenuCategoryServiceModel> GetMenu();

        IEnumerable<ListCategoriesServiceModel> All();
    }
}