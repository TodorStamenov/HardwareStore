namespace HardwareStore.Services.Infrastructure.Extensions
{
    using System;

    public static class DecimalExtensions
    {
        public static decimal PriceWithDiscount(this decimal price, int discount)
        {
            return (price * (1M - (discount / 100M))).RoundPrice();
        }

        public static decimal RoundPrice(this decimal price)
        {
            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }
}