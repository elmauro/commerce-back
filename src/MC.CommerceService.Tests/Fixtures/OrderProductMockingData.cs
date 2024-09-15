using AutoFixture;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the order product services.
    /// Uses AutoFixture to automatically create realistic and random data for testing.
    /// </summary>
    public static class OrderProductMockingData
    {
        public static Fixture fixture = new Fixture();

        static OrderProductMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new OrderProductRequest object with random values.
        /// </summary>
        /// <returns>A new OrderProductRequest with random values.</returns>
        public static OrderProductRequest GetOrderProductRequest()
        {
            return fixture.Build<OrderProductRequest>()
                .Create();
        }

        /// <summary>
        /// Generates a new OrderProduct object with unique identifiers and current timestamps.
        /// </summary>
        /// <returns>A new OrderProduct with unique IDs and current timestamps for created and last updated.</returns>
        public static OrderProduct GetOrderProduct()
        {
            return fixture.Build<OrderProduct>()
                .With(op => op.OrderId, Guid.NewGuid().ToString())
                .With(op => op.ProductId, Guid.NewGuid().ToString())
                .With(op => op.CreatedAt, DateTime.UtcNow)
                .With(op => op.LastUpdatedAt, DateTime.UtcNow)
                .Create();
        }
    }
}
