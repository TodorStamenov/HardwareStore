namespace HardwareStore.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.SubCategories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 20),
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.Items",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Model = c.String(nullable: false, maxLength: 50),
                    Description = c.String(nullable: false),
                    Manufacturer = c.String(nullable: false, maxLength: 50),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Discount = c.Decimal(precision: 18, scale: 2),
                    Quantity = c.Int(nullable: false),
                    WarrantyLength = c.Int(nullable: false),
                    Image = c.Binary(),
                    Views = c.Int(nullable: false),
                    UploadDate = c.DateTime(nullable: false),
                    SubCategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId);

            CreateTable(
                "dbo.Reviews",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    Mark = c.Int(nullable: false),
                    ReviewDate = c.DateTime(nullable: false),
                    AuthorId = c.Int(nullable: false),
                    ItemId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ItemId);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ProfileImage = c.Binary(),
                    Email = c.String(),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Int(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    DateAdded = c.DateTime(nullable: false),
                    ReviewId = c.Int(nullable: false),
                    AuthorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Reviews", t => t.ReviewId)
                .Index(t => t.ReviewId)
                .Index(t => t.AuthorId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.Int(nullable: false),
                    RoleId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.Sales",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 50),
                    LastName = c.String(nullable: false, maxLength: 50),
                    Phone = c.String(nullable: false),
                    Address = c.String(nullable: false, maxLength: 100),
                    TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Date = c.DateTime(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ItemSales",
                c => new
                {
                    ItemId = c.Int(nullable: false),
                    SaleId = c.Int(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => new { t.ItemId, t.SaleId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Sales", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.SaleId);

            CreateTable(
                "dbo.Logs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(),
                    TableName = c.String(),
                    LogType = c.Int(nullable: false),
                    TimeStamp = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Reviews", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Comments", "ReviewId", "dbo.Reviews");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.ItemSales", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.ItemSales", "ItemId", "dbo.Items");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.ItemSales", new[] { "SaleId" });
            DropIndex("dbo.ItemSales", new[] { "ItemId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Comments", new[] { "ReviewId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "ItemId" });
            DropIndex("dbo.Reviews", new[] { "AuthorId" });
            DropIndex("dbo.Items", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.SubCategories", new[] { "Name" });
            DropIndex("dbo.Categories", new[] { "Name" });
            DropTable("dbo.Logs");
            DropTable("dbo.ItemSales");
            DropTable("dbo.Sales");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Items");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}