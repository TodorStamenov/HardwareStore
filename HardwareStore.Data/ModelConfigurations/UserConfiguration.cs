namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasMany(u => u.Sales)
                .WithRequired(q => q.User)
                .HasForeignKey(q => q.UserId)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.Reviews)
                .WithRequired(q => q.Author)
                .HasForeignKey(q => q.AuthorId)
                .WillCascadeOnDelete(false);

            this.HasMany(u => u.Comments)
                .WithRequired(q => q.Author)
                .HasForeignKey(q => q.AuthorId)
                .WillCascadeOnDelete(false);
        }
    }
}