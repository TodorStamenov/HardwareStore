namespace HardwareStore.Data
{
    using IdentityModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using ModelConfigurations;
    using Models;
    using System.Data.Entity;

    public class HardwareStoreDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public HardwareStoreDbContext()
            : base("data source=.;initial catalog=HardwareStore;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HardwareStoreDbContext, Configuration>());
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configurations.Add(new CategoryConfiguration());
            builder.Configurations.Add(new SubCategoryConfiguration());
            builder.Configurations.Add(new ItemConfiguration());
            builder.Configurations.Add(new ReviewConfiguration());
            builder.Configurations.Add(new ItemSaleConfiguration());
            builder.Configurations.Add(new UserConfiguration());
        }
    }
}