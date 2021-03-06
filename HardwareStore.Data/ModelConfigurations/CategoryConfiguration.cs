﻿namespace HardwareStore.Data.ModelConfigurations
{
    using Models;
    using System.Data.Entity.ModelConfiguration;

    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.HasMany(c => c.SubCategories)
                .WithRequired(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId);

            this.HasIndex(c => c.Name)
                .IsUnique(true);
        }
    }
}