namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            this.HasMany(r => r.Comments)
                .WithRequired(c => c.Review)
                .HasForeignKey(c => c.ReviewId)
                .WillCascadeOnDelete(false);
        }
    }
}