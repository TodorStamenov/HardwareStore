namespace HardwareStore.Services.Areas.Moderator.Models.Items
{
    using Data;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class ItemFormServiceModel
    {
        [Required]
        [StringLength(
            DataConstants.ItemConstants.MaxNameLength,
            MinimumLength = DataConstants.ItemConstants.MinNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.ItemConstants.MinDescriptionLength)]
        public string Description { get; set; }

        [Range(
            DataConstants.ItemConstants.MinPrice,
            DataConstants.ItemConstants.MaxPrice)]
        public decimal Price { get; set; }

        // Percentage
        [Range(
            DataConstants.ItemConstants.MinDiscount,
            DataConstants.ItemConstants.MaxDiscount)]
        public int? Discount { get; set; }

        [Range(
            DataConstants.ItemConstants.MinQuantity,
            DataConstants.ItemConstants.MaxQuantity)]
        public int Quantity { get; set; }

        // In Months
        [Range(
            DataConstants.ItemConstants.MinWarranty,
            DataConstants.ItemConstants.MaxWarranty)]
        public int WarrantyLength { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}