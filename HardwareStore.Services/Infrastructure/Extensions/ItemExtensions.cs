namespace HardwareStore.Services.Infrastructure.Extensions
{
    using Data.Models;
    using Models.Items;
    using System.Collections.Generic;
    using System.Linq;

    public static class ItemExtensions
    {
        public static IQueryable<Item> Filter(this IQueryable<Item> items, string search)
        {
            if (search != null)
            {
                return items
                    .Where(i => i.Name.ToLower().Contains(search));
            }

            return items;
        }

        public static IEnumerable<ListItemsServiceModel> ProjectToListModel(this IQueryable<Item> items)
        {
            return items
                .AsEnumerable()
                .Select(i => new ListItemsServiceModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price.RoundDecimal(2),
                    Discount = i.Discount,
                    PriceWithDiscount = i.Price.PriceWithDiscount(i.Discount.GetValueOrDefault()),
                    UploadDate = i.UploadDate.ToLocalTime(),
                    Views = i.Views,
                    Reviews = i.Reviews.Count,
                    Image = i.Image.ConvertImage()
                });
        }
    }
}