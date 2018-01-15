namespace HardwareStore.Web.Areas.Moderator.Models.Sales
{
    using Infrastructure.Helpers;
    using Services.Areas.Moderator.Models.Sales;
    using System.Collections.Generic;

    public class ListSalesViewModel : BasePageViewModel
    {
        public string Search { get; set; }

        public IEnumerable<ListSalesServiceModel> Sales { get; set; }
    }
}