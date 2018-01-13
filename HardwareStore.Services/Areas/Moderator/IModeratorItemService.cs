using HardwareStore.Services.Areas.Moderator.Models.Items;

namespace HardwareStore.Services.Areas.Moderator
{
    public interface IModeratorItemService
    {
        bool NameExists(string name);

        string GetName(int id);

        int Create(
            string name,
            string description,
            decimal price,
            int? discount,
            int quantity,
            int warrantyLength,
            byte[] itemImage,
            int subCategoryId);

        bool Edit(
            int id,
            string name,
            string description,
            decimal price,
            int? discount,
            int quantity,
            int warrantyLength,
            byte[] itemImage,
            int subCategoryId);

        ItemFormServiceModel GetForm(int id);
    }
}