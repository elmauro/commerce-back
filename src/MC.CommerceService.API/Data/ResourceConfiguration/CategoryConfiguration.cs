using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    public class CategoryConfiguration : DatabaseResourceConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            // Use basic settings from the general mssql configuration for categories
            base.Configure(builder);

            // Make sure the category ID is saved as a GUID (a type of unique identifier) in the database.
            builder.Property(b => b.CategoryId).HasConversion<Guid>();
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(255);

            // Seeding categories
            builder.HasData(
                new Category { CategoryId = category1Id, CategoryName = "Electronics", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Category { CategoryId = category2Id, CategoryName = "Books", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Category { CategoryId = category3Id, CategoryName = "Clothing", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Category { CategoryId = category4Id, CategoryName = "Home & Kitchen", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Category { CategoryId = category5Id, CategoryName = "Sports", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );
        }
    }
}
