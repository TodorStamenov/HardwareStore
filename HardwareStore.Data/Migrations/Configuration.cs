namespace HardwareStore.Data.Migrations
{
    using Common;
    using IdentityModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<HardwareStoreDbContext>
    {
        private const int AdminsCount = 1;
        private const int ModeratorsCount = 3;
        private const int UsersCount = 50;
        private const int CategoriesCount = 3;
        private const int SubCategoriesCount = CategoriesCount * 3;
        private const int ItemsCount = SubCategoriesCount * 20;
        private const int ReviewsCount = ItemsCount * 10;
        private const int CommentsCount = ItemsCount;
        private const int MinViewsPerItem = 0;
        private const int MaxViewsPerItem = 200;
        private const int MinQuantity = 0;
        private const int MaxQuantity = 200;
        private const int MinPrice = 5;
        private const int MaxPrice = 5000;
        private const int MinDiscount = DataConstants.ItemConstants.MinDiscount;
        private const int MaxDiscount = DataConstants.ItemConstants.MaxDiscount;
        private const int MinWarranty = DataConstants.ItemConstants.MinWarranty;
        private const int MaxWarranty = DataConstants.ItemConstants.MaxWarranty;
        private const int SalesCount = 100;
        private const int MinItemsPerSale = 1;
        private const int MaxItemsPerSale = 6;

        private static readonly Random random = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HardwareStoreDbContext context)
        {
            var roleStore = new RoleStore<Role, int, UserRole>(context);
            var roleManager = new RoleManager<Role, int>(roleStore);

            var userStore = new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context);
            var userManager = new UserManager<User, int>(userStore);

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            Assembly assembly = CommonConstants.webAssembly;

            Task.Run(async () =>
            {
                await this.SeedRolesAsync(roleManager, context);
                await this.SeedUsersAsync(userManager, UsersCount, context);
                await this.SeedUsersAsync(userManager, roleManager, AdminsCount, CommonConstants.AdminRole, context);
                await this.SeedUsersAsync(userManager, roleManager, ModeratorsCount, CommonConstants.ModeratorRole, context);
                await this.SeedCategoriesAsync(CategoriesCount, context);
                await this.SeedSubCategoriesAsync(SubCategoriesCount, context);
                await this.SeedItemsAsync(ItemsCount, context);
                await this.SeedReviewsAsync(ReviewsCount, context);
                await this.SeedCommentsAsync(CommentsCount, context);
                await this.SeedSalesAsync(SalesCount, context);
            })
            .GetAwaiter()
            .GetResult();
        }

        private async Task SeedRolesAsync(RoleManager<Role, int> roleManager, HardwareStoreDbContext context)
        {
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            await roleManager.CreateAsync(new Role { Name = CommonConstants.AdminRole });
            await roleManager.CreateAsync(new Role { Name = CommonConstants.ModeratorRole });

            await context.SaveChangesAsync();
        }

        private async Task SeedUsersAsync(UserManager<User, int> userManager, int usersCount, HardwareStoreDbContext context)
        {
            if (await context.Users.AnyAsync(u => !u.Roles.Any()))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                string username = $"Username{i}";

                User user = new User
                {
                    UserName = username,
                    Email = $"{username}@{username}.com",
                    ProfileImage = CommonConstants.defaultUserImage
                };

                await userManager.CreateAsync(user, "123");
                await context.SaveChangesAsync();
            }
        }

        private async Task SeedUsersAsync(
            UserManager<User, int> userManager,
            RoleManager<Role, int> roleManager,
            int usersCount,
            string role,
            HardwareStoreDbContext context)
        {
            if (await context.Users.AnyAsync(u => u.Roles.Any(r => r.Role.Name == role)))
            {
                return;
            }

            for (int i = 1; i <= usersCount; i++)
            {
                string username = $"{role}{i}";

                User user = new User
                {
                    UserName = username,
                    Email = $"{username}@{username}.com",
                    ProfileImage = CommonConstants.defaultUserImage
                };

                await userManager.CreateAsync(user, "123");
                await userManager.AddToRoleAsync(user.Id, role);
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedCategoriesAsync(int categoriesCount, HardwareStoreDbContext context)
        {
            if (await context.Categories.AnyAsync())
            {
                return;
            }

            for (int i = 1; i <= categoriesCount; i++)
            {
                context.Categories.Add(new Category
                {
                    Name = $"Category Name {i}"
                });
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedSubCategoriesAsync(int subCategoriesCount, HardwareStoreDbContext context)
        {
            if (await context.SubCategories.AnyAsync())
            {
                return;
            }

            List<int> categoryIds = await context.Categories.Select(c => c.Id).ToListAsync();

            for (int i = 1; i <= subCategoriesCount; i++)
            {
                context.SubCategories.Add(new SubCategory
                {
                    Name = $"Sub Category Name {i}",
                    CategoryId = categoryIds[random.Next(0, categoryIds.Count)]
                });
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedItemsAsync(int itemsCount, HardwareStoreDbContext context)
        {
            if (await context.Items.AnyAsync())
            {
                return;
            }

            List<int> subCategoryIds = await context.SubCategories.Select(c => c.Id).ToListAsync();
            List<User> users = await context.Users.ToListAsync();

            for (int i = 1; i <= itemsCount; i++)
            {
                int? discount = this.GetRandomBool()
                    ? (int?)random.Next(MinDiscount, MaxDiscount)
                    : null;

                Item item = new Item
                {
                    Name = $"Item Name {i}",
                    Description = CommonConstants.lorem,
                    Quantity = random.Next(MinQuantity, MaxQuantity),
                    Price = random.Next(MinPrice, MaxPrice),
                    Discount = discount,
                    WarrantyLength = random.Next(MinWarranty, MaxWarranty),
                    Image = random.Next(0, 2) == 0 ? CommonConstants.amdImage : CommonConstants.intelImage,
                    UploadDate = DateTime.UtcNow.AddDays(-i).AddHours(-i).AddMinutes(-i),
                    Views = random.Next(MinViewsPerItem, MaxViewsPerItem),
                    SubCategoryId = subCategoryIds[random.Next(0, subCategoryIds.Count)]
                };

                context.Items.Add(item);
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedReviewsAsync(int reviewsCount, HardwareStoreDbContext context)
        {
            if (await context.Reviews.AnyAsync())
            {
                return;
            }

            List<User> users = await context.Users.ToListAsync();
            var itemInfo = await context
                .Items
                .Select(q => new
                {
                    q.Id,
                    q.UploadDate
                })
                .ToListAsync();

            for (int i = 0; i < reviewsCount; i++)
            {
                var item = itemInfo[random.Next(0, itemInfo.Count)];

                Review review = new Review
                {
                    Content = CommonConstants.lorem.Substring(0, CommonConstants.lorem.Length / 2),
                    AuthorId = users[random.Next(0, users.Count)].Id,
                    DateAdded = item.UploadDate.AddHours(i).AddMinutes(i),
                    ItemId = item.Id,
                    Mark = (Mark)random.Next(0, Enum.GetValues(typeof(Mark)).Length),
                };

                context.Reviews.Add(review);
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedCommentsAsync(int commentsCount, HardwareStoreDbContext context)
        {
            if (await context.Comments.AnyAsync())
            {
                return;
            }

            List<User> users = await context.Users.ToListAsync();
            var reviewInfo = await context
                .Reviews
                .Select(q => new
                {
                    q.Id,
                    q.DateAdded,
                })
                .ToListAsync();

            for (int i = 0; i < commentsCount; i++)
            {
                var review = reviewInfo[random.Next(0, reviewInfo.Count)];

                Comment comment = new Comment
                {
                    Content = CommonConstants.lorem.Substring(0, CommonConstants.lorem.Length / 4),
                    AuthorId = users[random.Next(0, users.Count)].Id,
                    DateAdded = review.DateAdded.AddHours(i).AddMinutes(i),
                    ReviewId = review.Id
                };

                context.Comments.Add(comment);
            }

            await context.SaveChangesAsync();
        }

        private async Task SeedSalesAsync(int salesCount, HardwareStoreDbContext context)
        {
            if (await context.Sales.AnyAsync())
            {
                return;
            }

            List<int> userIds = await context.Users.Select(u => u.Id).ToListAsync();
            List<Item> items = await context.Items.ToListAsync();

            for (int i = 1; i <= salesCount; i++)
            {
                Sale sale = new Sale
                {
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Phone = $"+359885612{random.Next(100, 1000)}",
                    Address = $"Address {i}",
                    UserId = userIds[random.Next(0, userIds.Count)]
                };

                int itemsCount = random.Next(MinItemsPerSale, MaxItemsPerSale);

                for (int j = 0; j < itemsCount; j++)
                {
                    Item item = items[random.Next(0, items.Count)];

                    if (sale.Items.Any(itemSale => itemSale.ItemId == item.Id)
                        || item.Quantity <= 0)
                    {
                        j--;
                        continue;
                    }

                    int quantity = random.Next(1, item.Quantity + 1);

                    sale.Items.Add(new ItemSale
                    {
                        Item = item,
                        ItemId = item.Id,
                        Quantity = quantity,
                        Price = quantity * item.Price * (1M - (item.Discount.GetValueOrDefault() / 100M))
                    });

                    item.Quantity -= quantity;

                    await context.SaveChangesAsync();
                }

                sale.Date = sale
                    .Items
                    .Select(itemSale => itemSale.Item.UploadDate)
                    .Max()
                    .AddDays(i);

                sale.TotalPrice = sale
                    .Items
                    .Sum(itemSale => itemSale.Price);

                context.Sales.Add(sale);
            }

            await context.SaveChangesAsync();
        }

        private bool GetRandomBool()
        {
            return random.Next(0, 2) == 0 ? false : true;
        }
    }
}