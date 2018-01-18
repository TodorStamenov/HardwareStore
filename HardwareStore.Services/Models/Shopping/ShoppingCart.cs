namespace HardwareStore.Services.Models.Shopping
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCart
    {
        private readonly IList<CartItem> items;

        public ShoppingCart()
        {
            this.items = new List<CartItem>();
        }

        public IEnumerable<CartItem> Items
        {
            get { return this.items; }
        }

        public void AddToCart(int itemId, int quantity)
        {
            CartItem cartItem = this.items
                .FirstOrDefault(i => i.Id == itemId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Id = itemId,
                    Quantity = quantity
                };

                this.items.Add(cartItem);
            }

            cartItem.Quantity = quantity;
        }

        public void RemoveFromCart(int itemId)
        {
            CartItem cartItem = this.items
                .FirstOrDefault(i => i.Id == itemId);

            if (cartItem != null)
            {
                this.items.Remove(cartItem);
            }
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}