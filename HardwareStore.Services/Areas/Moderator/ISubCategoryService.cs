namespace HardwareStore.Services.Areas.Moderator
{
    using Models.SubCategories;
    using Services.Models.Categories;
    using System.Collections.Generic;

    public interface ISubCategoryService
    {
        bool Exists(int id);

        bool HasItems(int id);

        bool NameExists(string name);

        string GetName(int id);

        bool Create(int categoryId, string name);

        bool Edit(int id, int categoryId, string name);

        SubCategoryFormServiceModel GetForm(int id);

        IEnumerable<MenuCategoryServiceModel> GetMenu();
    }
}