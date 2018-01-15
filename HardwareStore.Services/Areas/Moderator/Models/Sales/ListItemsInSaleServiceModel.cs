namespace HardwareStore.Services.Areas.Moderator.Models.Sales
{
    public class ListItemsInSaleServiceModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public decimal SingleItemPrice { get; set; }

        public string Image { get; set; }
    }
}