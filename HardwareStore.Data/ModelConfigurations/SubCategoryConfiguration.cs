namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class SubCategoryConfiguration : EntityTypeConfiguration<SubCategory>
    {
        public SubCategoryConfiguration()
        {
            this.HasMany(sc => sc.Items)
                .WithRequired(q => q.SubCategory)
                .HasForeignKey(q => q.SubCategoryId);

            this.HasIndex(sc => sc.Name)
                .IsUnique(true);
        }
    }
}