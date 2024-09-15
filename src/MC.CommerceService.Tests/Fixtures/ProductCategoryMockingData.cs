using AutoFixture;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.Tests.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the product category services.
    /// Uses AutoFixture to automatically create realistic and random data for testing.
    /// </summary>
    public static class ProductCategoryMockingData
    {
        public static Fixture fixture = new Fixture();

        static ProductCategoryMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new ProductCategoryRequest object with random values.
        /// </summary>
        /// <returns>A new ProductCategoryRequest with random values.</returns>
        public static ProductCategoryRequest GetProductCategoryRequest()
        {
            return fixture.Build<ProductCategoryRequest>()
                .Create();
        }

        /// <summary>
        /// Generates a new ProductCategory object with unique identifiers and current timestamps.
        /// </summary>
        /// <returns>A new ProductCategory with unique IDs and current timestamps for created and last updated.</returns>
        public static ProductCategory GetProductCategory()
        {
            return fixture.Build<ProductCategory>()
                .With(pc => pc.ProductId, Guid.NewGuid().ToString())
                .With(pc => pc.CategoryId, Guid.NewGuid().ToString())
                .With(pc => pc.CreatedAt, DateTime.UtcNow)
                .With(pc => pc.LastUpdatedAt, DateTime.UtcNow)
                .Create();
        }
    }
}
