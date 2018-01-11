namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class ItemSaleConfiguration : EntityTypeConfiguration<ItemSale>
    {
        public ItemSaleConfiguration()
        {
            this.HasKey(itemSale => new { itemSale.ItemId, itemSale.SaleId });

            this.HasRequired(itemSale => itemSale.Item)
                .WithMany(i => i.Sales)
                .HasForeignKey(itemSale => itemSale.ItemId);

            this.HasRequired(itemSale => itemSale.Sale)
                .WithMany(i => i.Items)
                .HasForeignKey(itemSale => itemSale.SaleId);
        }
    }
}