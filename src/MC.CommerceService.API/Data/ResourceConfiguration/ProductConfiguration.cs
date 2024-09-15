using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    /// <summary>
    /// Sets up how product data is saved in the database.
    /// This setup includes special rules for products, like how to save product IDs.
    /// </summary>
    public class ProductConfiguration : DatabaseResourceConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            // Use basic settings from the general mssql configuration for products
            base.Configure(builder);

            // Make sure the product ID is saved as a GUID (a type of unique identifier) in the database.
            builder.Property(b => b.ProductId).HasConversion<Guid>();

            builder.Property(p => p.Price)
               .HasPrecision(18, 2); // Example: total 18 digits, 2 decimal places

            // Seeding products
            builder.HasData(
                new Product { ProductId = product1Id, Title = "Laptop", Code = "P001", Description = "A powerful laptop", Price = 1000, Stock = 50, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product2Id, Title = "Smartphone", Code = "P002", Description = "A modern smartphone", Price = 800, Stock = 30, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product3Id, Title = "Tablet", Code = "P003", Description = "A versatile tablet", Price = 500, Stock = 20, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product4Id, Title = "Headphones", Code = "P004", Description = "Noise-canceling headphones", Price = 200, Stock = 100, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product5Id, Title = "Smartwatch", Code = "P005", Description = "A stylish smartwatch", Price = 250, Stock = 60, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product6Id, Title = "Monitor", Code = "P006", Description = "A 24-inch monitor", Price = 300, Stock = 40, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product7Id, Title = "Keyboard", Code = "P007", Description = "Mechanical keyboard", Price = 100, Stock = 70, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product8Id, Title = "Mouse", Code = "P008", Description = "Wireless mouse", Price = 50, Stock = 80, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product9Id, Title = "Chair", Code = "P009", Description = "Ergonomic office chair", Price = 150, Stock = 25, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product10Id, Title = "Desk", Code = "P010", Description = "Adjustable standing desk", Price = 500, Stock = 20, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product11Id, Title = "Printer", Code = "P011", Description = "Wireless printer", Price = 200, Stock = 15, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product12Id, Title = "Scanner", Code = "P012", Description = "Document scanner", Price = 150, Stock = 30, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product13Id, Title = "Webcam", Code = "P013", Description = "HD webcam", Price = 80, Stock = 50, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product14Id, Title = "Router", Code = "P014", Description = "Wireless router", Price = 120, Stock = 45, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Product { ProductId = product15Id, Title = "External Hard Drive", Code = "P015", Description = "1TB external hard drive", Price = 90, Stock = 100, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );
        }
    }
}
