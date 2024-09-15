using Microsoft.EntityFrameworkCore;
using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.Data
{
    /// <summary>
    /// Provides context for commerce database interactions, encapsulating configuration
    /// and functionality for accessing the database through Entity Framework Core.
    /// </summary>
    /// <remarks>
    /// This context is configured to use specific database options provided during instantiation
    /// and applies entity configurations dynamically from all configurations defined in the assembly.
    /// </remarks>
    public class CommerceDBContext(DbContextOptions<CommerceDBContext> options) : DbContext(options)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommerceDBContext"/> class.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .ApplyConfigurationsFromAssembly(typeof(CommerceDBContext).Assembly);
        }
    }
}
