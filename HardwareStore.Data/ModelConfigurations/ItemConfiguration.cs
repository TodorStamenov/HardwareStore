namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class ItemConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemConfiguration()
        {
            this.HasMany(i => i.Reviews)
                .WithRequired(r => r.Item)
                .HasForeignKey(r => r.ItemId);
        }
    }
}