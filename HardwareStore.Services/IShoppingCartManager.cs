namespace HardwareStore.Services
{
    using System.Collections.Generic;

    public interface IShoppingCartManager
    {
        void AddToCart(string id, int itemId, int quantity);

        void RemoveFromCart(string id, int itemId);

        void Clear(string id);

        Dictionary<int, int> GetItems(string id);
    }
}