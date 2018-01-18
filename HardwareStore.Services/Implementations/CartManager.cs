namespace HardwareStore.Services.Implementations
{
    using Data;
    using Models.Shopping;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class CartManager : IShoppingCartManager
    {
        private readonly HardwareStoreDbContext db;
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public CartManager(HardwareStoreDbContext db)
        {
            this.db = db;
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int itemId, int quantity)
        {
            this.GetShoppingCart(id).AddToCart(itemId, quantity);
        }

        public void RemoveFromCart(string id, int itemId)
        {
            this.GetShoppingCart(id).RemoveFromCart(itemId);
        }

        public void Clear(string id)
        {
            this.GetShoppingCart(id).Clear();
        }

        public Dictionary<int, int> GetItems(string id)
        {
            return this.GetShoppingCart(id)
                .Items
                .ToDictionary(i => i.Id, i => i.Quantity);
        }

        private ShoppingCart GetShoppingCart(string id)
        {
            return this.carts.GetOrAdd(id, new ShoppingCart());
        }
    }
}