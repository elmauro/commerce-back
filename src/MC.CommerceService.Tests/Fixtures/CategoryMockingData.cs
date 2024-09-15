using AutoFixture;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the category services.
    /// Uses AutoFixture to automatically create realistic and random data for testing.
    /// </summary>
    public static class CategoryMockingData
    {
        public static Fixture fixture = new Fixture();

        static CategoryMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new CategoryRequest object with random values.
        /// </summary>
        /// <returns>A new CategoryRequest with random values.</returns>
        public static CategoryRequest GetCategoryRequest()
        {
            return fixture.Build<CategoryRequest>()
                .Create();
        }

        /// <summary>
        /// Generates a new Category object with a unique identifier and current timestamps.
        /// </summary>
        /// <returns>A new Category with unique ID and current timestamps for created and last updated.</returns>
        public static Category GetCategory()
        {
            return fixture.Build<Category>()
                .With(c => c.CategoryId, Guid.NewGuid().ToString())
                .With(c => c.CreatedAt, DateTime.UtcNow)
                .With(c => c.LastUpdatedAt, DateTime.UtcNow)
                .Create();
        }
    }
}
