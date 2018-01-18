namespace HardwareStore.Web.Infrastructure.Extensions
{
    using System;
    using System.Web;

    public static class SessionExtensions
    {
        private const string ShoppingCartId = "Shopping_Cart_Id";

        public static string GetShoppingCartId(this HttpSessionStateBase session)
        {
            string shoppingCartId = session[ShoppingCartId] as string;

            if (shoppingCartId == null)
            {
                shoppingCartId = Guid.NewGuid().ToString();
                session[ShoppingCartId] = shoppingCartId;
            }

            return shoppingCartId;
        }
    }
}