namespace HardwareStore.Services.Models.Shopping
{
    public class ListCartItemsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public decimal Price { get; set; }

        public int? Discount { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public int Quantity { get; set; }
    }
}