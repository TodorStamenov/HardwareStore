namespace HardwareStore.Services.Areas.Moderator.Models.Sales
{
    using System.Collections.Generic;

    public class SaleDetailsServiceModel : ListSalesServiceModel
    {
        public string ProfileImage { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int ItemsCount { get; set; }

        public int TotalItems { get; set; }

        public IEnumerable<ListItemsInSaleServiceModel> Items { get; set; }
    }
}