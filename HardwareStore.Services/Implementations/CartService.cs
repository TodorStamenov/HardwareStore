namespace HardwareStore.Services.Implementations
{
    using Data;
    using Infrastructure.Extensions;
    using Models.Shopping;
    using System.Collections.Generic;
    using System.Linq;

    public class CartService : ICartService
    {
        private readonly HardwareStoreDbContext db;

        public CartService(HardwareStoreDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ListCartItemsServiceModel> GetItems(Dictionary<int, int> items)
        {
            return this.db
                .Items
                .Where(i => items.Keys.Contains(i.Id))
                .OrderByDescending(i => i.Price)
                .AsEnumerable()
                .Select(i => new ListCartItemsServiceModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price.RoundPrice(),
                    Quantity = items[i.Id] > i.Quantity ? i.Quantity : items[i.Id],
                    Image = i.Image.ConvertImage(),
                    Discount = i.Discount,
                    PriceWithDiscount = i.Price.PriceWithDiscount(i.Discount.GetValueOrDefault())
                })
                .ToList();
        }
    }
}