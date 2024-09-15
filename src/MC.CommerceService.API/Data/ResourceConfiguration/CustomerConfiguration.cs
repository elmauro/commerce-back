using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MC.CommerceService.API.Data.ResourceConfiguration
{
    public class CustomerConfiguration : DatabaseResourceConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            // Use basic settings from the general mssql configuration for customers
            base.Configure(builder);

            // Make sure the customer ID is saved as a GUID (a type of unique identifier) in the database.
            builder.Property(b => b.CustomerId).HasConversion<Guid>();
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(255);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(255);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
            builder.Property(c => c.Phone).HasMaxLength(20);

            // Ensure Email is unique
            builder.HasIndex(c => c.Email).IsUnique();

            // Seeding customers
            builder.HasData(
                new Customer { CustomerId = customer1Id, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "123-456-7890", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Customer { CustomerId = customer2Id, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "234-567-8901", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Customer { CustomerId = customer3Id, FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@example.com", Phone = "345-678-9012", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Customer { CustomerId = customer4Id, FirstName = "Emily", LastName = "Williams", Email = "emily.williams@example.com", Phone = "456-789-0123", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow },
                new Customer { CustomerId = customer5Id, FirstName = "Chris", LastName = "Brown", Email = "chris.brown@example.com", Phone = "567-890-1234", CreatedAt = DateTimeOffset.UtcNow, LastUpdatedAt = DateTimeOffset.UtcNow }
            );

        }
    }
}
