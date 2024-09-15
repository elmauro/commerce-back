using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    public class ProductCategoryConfiguration : DatabaseResourceConfiguration<ProductCategory>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            // Use basic settings from the general MSSQL configuration for products and categories
            base.Configure(builder);

            // Configure composite key using both ProductId and CategoryId
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            // Configure relationships
            builder.HasOne(pc => pc.Product)
                   .WithMany(p => p.ProductCategories)
                   .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.Category)
                   .WithMany(c => c.ProductCategories)
                   .HasForeignKey(pc => pc.CategoryId);

            // Seeding product category
            builder.HasData(
                // Linking Product 1 with Categories
                new ProductCategory { ProductId = product1Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product1Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 2 with Categories
                new ProductCategory { ProductId = product2Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product2Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 3 with Categories
                new ProductCategory { ProductId = product3Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product3Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 4 with Categories
                new ProductCategory { ProductId = product4Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product4Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 5 with Categories
                new ProductCategory { ProductId = product5Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product5Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 6 with Categories
                new ProductCategory { ProductId = product6Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product6Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 7 with Categories
                new ProductCategory { ProductId = product7Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product7Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 8 with Categories
                new ProductCategory { ProductId = product8Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product8Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 9 with Categories
                new ProductCategory { ProductId = product9Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product9Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 10 with Categories
                new ProductCategory { ProductId = product10Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product10Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 11 with Categories
                new ProductCategory { ProductId = product11Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product11Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 12 with Categories
                new ProductCategory { ProductId = product12Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product12Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 13 with Categories
                new ProductCategory { ProductId = product13Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product13Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 14 with Categories
                new ProductCategory { ProductId = product14Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product14Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Linking Product 15 with Categories
                new ProductCategory { ProductId = product15Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product15Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },

                // Additional rows to make a total of 25 ProductCategory rows
                new ProductCategory { ProductId = product1Id, CategoryId = category3Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product2Id, CategoryId = category4Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product3Id, CategoryId = category5Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product4Id, CategoryId = category1Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new ProductCategory { ProductId = product5Id, CategoryId = category2Id, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );

        }
    }
}
