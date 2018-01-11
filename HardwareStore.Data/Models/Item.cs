namespace HardwareStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ItemConstants.MinNameLength)]
        [MaxLength(DataConstants.ItemConstants.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.ItemConstants.MinModelLength)]
        [MaxLength(DataConstants.ItemConstants.MaxModelLength)]
        public string Model { get; set; }

        [Required]
        [MinLength(DataConstants.ItemConstants.MinDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [MinLength(DataConstants.ItemConstants.MinManufacturerLength)]
        [MaxLength(DataConstants.ItemConstants.MaxManufacturerLength)]
        public string Manufacturer { get; set; }

        [Range(
            DataConstants.ItemConstants.MinPrice,
            DataConstants.ItemConstants.MaxPrice)]
        public decimal Price { get; set; }

        // Percentage
        [Range(
            DataConstants.ItemConstants.MinDiscount,
            DataConstants.ItemConstants.MaxDiscount)]
        public decimal? Discount { get; set; }

        [Range(
            DataConstants.ItemConstants.MinQuantity,
            DataConstants.ItemConstants.MaxQuantity)]
        public int Quantity { get; set; }

        // In Months
        [Range(
            DataConstants.ItemConstants.MinWarranty,
            DataConstants.ItemConstants.MaxWarranty)]
        public int WarrantyLength { get; set; }

        [MaxLength(DataConstants.ItemConstants.MaxItemImageSize)]
        public byte[] Image { get; set; }

        [Range(
            DataConstants.ItemConstants.MinViewsCount,
            DataConstants.ItemConstants.MaxViewsCount)]
        public int Views { get; set; }

        public DateTime UploadDate { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual List<Review> Reviews { get; set; } = new List<Review>();

        public virtual List<ItemSale> Sales { get; set; } = new List<ItemSale>();
    }
}