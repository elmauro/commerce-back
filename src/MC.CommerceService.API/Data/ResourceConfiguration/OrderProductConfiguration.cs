using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    public class OrderProductConfiguration : DatabaseResourceConfiguration<OrderProduct>
    {
        public override void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            // Use basic settings from the general mssql configuration for products
            base.Configure(builder);

            // Make sure the order product ID is saved as a GUID (a type of unique identifier) in the database.
            builder.HasKey(op => new { op.OrderId, op.ProductId });

            // Configure relationships
            builder.HasOne(op => op.Order)
                   .WithMany(o => o.OrderProducts)
                   .HasForeignKey(op => op.OrderId);

            builder.HasOne(op => op.Product)
                   .WithMany(p => p.OrderProducts)
                   .HasForeignKey(op => op.ProductId);

            // Configure Quantity field
            builder.Property(op => op.Quantity).IsRequired();


            // Seeding order product
            builder.HasData(
                new OrderProduct { OrderId = order1Id, ProductId = product1Id, Quantity = 2, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order1Id, ProductId = product2Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order1Id, ProductId = product3Id, Quantity = 3, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order2Id, ProductId = product4Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order2Id, ProductId = product5Id, Quantity = 2, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order2Id, ProductId = product6Id, Quantity = 3, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order3Id, ProductId = product7Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order3Id, ProductId = product8Id, Quantity = 2, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order4Id, ProductId = product9Id, Quantity = 3, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order4Id, ProductId = product10Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order4Id, ProductId = product11Id, Quantity = 2, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order5Id, ProductId = product12Id, Quantity = 3, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order5Id, ProductId = product13Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order5Id, ProductId = product14Id, Quantity = 2, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new OrderProduct { OrderId = order5Id, ProductId = product15Id, Quantity = 1, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );
        }
    }
}
