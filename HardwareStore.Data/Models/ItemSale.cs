using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Data.Models
{
    public class ItemSale
    {
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int SaleId { get; set; }

        public virtual Sale Sale { get; set; }

        [Range(
            DataConstants.ItemSaleConstants.MinQuantity,
            DataConstants.ItemSaleConstants.MaxQuantity)]
        public int Quantity { get; set; }

        [Range(
            DataConstants.ItemSaleConstants.MinPrice,
            DataConstants.ItemSaleConstants.MaxPrice)]
        public decimal Price { get; set; }
    }
}