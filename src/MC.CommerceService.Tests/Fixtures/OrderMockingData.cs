using AutoFixture;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the order services.
    /// Uses AutoFixture to automatically create realistic and random data for testing.
    /// </summary>
    public static class OrderMockingData
    {
        public static Fixture fixture = new Fixture();

        static OrderMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new OrderRequest object with random values.
        /// </summary>
        /// <returns>A new OrderRequest with random values.</returns>
        public static OrderRequest GetOrderRequest()
        {
            return fixture.Build<OrderRequest>()
                .Create();
        }

        /// <summary>
        /// Generates a new Order object with unique identifiers and current timestamps.
        /// </summary>
        /// <returns>A new Order with unique ID and current timestamps for created and last updated.</returns>
        public static Order GetOrder()
        {
            return fixture.Build<Order>()
                .With(o => o.OrderId, Guid.NewGuid().ToString())
                .With(o => o.CustomerId, Guid.NewGuid().ToString())
                .With(o => o.CreatedAt, DateTime.UtcNow)
                .With(o => o.LastUpdatedAt, DateTime.UtcNow)
                .Create();
        }
    }
}
