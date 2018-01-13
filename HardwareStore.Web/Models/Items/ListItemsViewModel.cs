namespace HardwareStore.Web.Models.Items
{
    using Infrastructure.Helpers;
    using Services.Models.Items;
    using System.Collections.Generic;

    public class ListItemsViewModel : BasePageViewModel
    {
        public string Search { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public IEnumerable<ListItemsServiceModel> Items { get; set; }
    }
}