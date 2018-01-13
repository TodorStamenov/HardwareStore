namespace HardwareStore.Web.Areas.Moderator.Models.Items
{
    using Services.Areas.Moderator.Models.Items;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class ItemFormViewModel
    {
        public ItemFormServiceModel Item { get; set; }

        [Display(Name = "Sub Category")]
        public int SubCategoryId { get; set; }

        public IEnumerable<SelectListItem> SubCategories { get; set; }
    }
}