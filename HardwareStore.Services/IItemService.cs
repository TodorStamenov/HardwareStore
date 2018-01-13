namespace HardwareStore.Services
{
    using Models.Items;
    using System.Collections.Generic;

    public interface IItemService
    {
        int Total(string search);

        int TotalByCategory(int categoryId);

        int TotalBySubCategory(int subCategoryId);

        ItemDetailsServiceModel Details(int id);

        IEnumerable<ListItemsServiceModel> ByCategory(int page, int pageSize, int categoryId);

        IEnumerable<ListItemsServiceModel> BySubCategory(int page, int pageSize, int subCategoryId);

        IEnumerable<ListItemsServiceModel> All(int page, int pageSize, string search);
    }
}