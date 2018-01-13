namespace HardwareStore.Web.Models.Items
{
    using Infrastructure.Helpers;
    using Services.Models.Items;
    using Services.Models.Reviews;
    using System.Collections.Generic;

    public class ItemDetailsViewModel : BasePageViewModel
    {
        public ItemDetailsServiceModel Item { get; set; }

        public IEnumerable<ListReviewsServiceModel> Reviews { get; set; }
    }
}