namespace HardwareStore.Services.Models.Items
{
    public class ItemDetailsServiceModel : BaseItemServiceModel
    {
        public int SubCategoryId { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public int WarrantyLength { get; set; }

        public double Rating { get; set; }
    }
}