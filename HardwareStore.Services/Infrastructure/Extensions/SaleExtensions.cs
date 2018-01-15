namespace HardwareStore.Services.Infrastructure.Extensions
{
    using Data.Models;
    using System.Linq;

    public static class SaleExtensions
    {
        public static IQueryable<Sale> Filter(this IQueryable<Sale> sales, string search)
        {
            if (!string.IsNullOrEmpty(search)
                || !string.IsNullOrWhiteSpace(search))
            {
                return sales
                    .Where(s => s.User.UserName.ToLower().Contains(search.ToLower()));
            }

            return sales;
        }
    }
}