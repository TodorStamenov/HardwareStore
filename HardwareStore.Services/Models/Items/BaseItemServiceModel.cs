namespace HardwareStore.Services.Models.Items
{
    using System;

    public class BaseItemServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? Discount { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public string Image { get; set; }

        public DateTime UploadDate { get; set; }
    }
}