namespace HardwareStore.Services.Infrastructure.Extensions
{
    using System;

    public static class DecimalExtensions
    {
        public static decimal PriceWithDiscount(this decimal price, int discount)
        {
            return (price * (1M - (discount / 100M))).RoundDecimal(2);
        }

        public static decimal RoundDecimal(this decimal price, int signs)
        {
            return Math.Round(price, signs, MidpointRounding.AwayFromZero);
        }
    }
}