using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    public class OrderConfiguration : DatabaseResourceConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            // Use basic settings from the general mssql configuration for orders
            base.Configure(builder);

            // Make sure the order ID is saved as a GUID (a type of unique identifier) in the database.
            builder.Property(b => b.OrderId).HasConversion<Guid>();

            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount).IsRequired().HasColumnType("decimal(10, 2)");

            // Configure relationships
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seeding orders
            builder.HasData(
                new Order { OrderId = order1Id, CustomerId = customer1Id, OrderDate = DateTime.UtcNow, TotalAmount = 500, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Order { OrderId = order2Id, CustomerId = customer2Id, OrderDate = DateTime.UtcNow, TotalAmount = 1000, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Order { OrderId = order3Id, CustomerId = customer3Id, OrderDate = DateTime.UtcNow, TotalAmount = 750, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Order { OrderId = order4Id, CustomerId = customer4Id, OrderDate = DateTime.UtcNow, TotalAmount = 900, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Order { OrderId = order5Id, CustomerId = customer5Id, OrderDate = DateTime.UtcNow, TotalAmount = 1200, CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );
        }
    }
}
