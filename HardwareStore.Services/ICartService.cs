namespace HardwareStore.Services
{
    using Models.Shopping;
    using System.Collections.Generic;

    public interface ICartService
    {
        IEnumerable<ListCartItemsServiceModel> GetItems(Dictionary<int, int> items);
    }
}