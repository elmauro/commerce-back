using AutoFixture;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;

namespace MC.Insurance.ApplicationServicesTest.Fixtures
{
    /// <summary>
    /// Provides fake data for testing the customer services.
    /// Uses AutoFixture to automatically create realistic and random data for testing.
    /// </summary>
    public static class CustomerMockingData
    {
        public static Fixture fixture = new Fixture();

        static CustomerMockingData()
        {
            fixture = new Fixture();

            // Handle circular references by omitting recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Creates a new CustomerRequest object with random values.
        /// </summary>
        /// <returns>A new CustomerRequest with random values.</returns>
        public static CustomerRequest GetCustomerRequest()
        {
            return fixture.Build<CustomerRequest>()
                .With(c => c.Phone, GenerateValidPhoneNumber())
                .With(c => c.Email, GenerateValidEmail())
                .Create();
        }

        /// <summary>
        /// Generates a new Customer object with unique identifiers and current timestamps.
        /// </summary>
        /// <returns>A new Customer with unique ID and current timestamps for created and last updated.</returns>
        public static Customer GetCustomer()
        {
            return fixture.Build<Customer>()
                .With(c => c.CustomerId, Guid.NewGuid().ToString())
                .With(c => c.CreatedAt, DateTime.UtcNow)
                .With(c => c.LastUpdatedAt, DateTime.UtcNow)
                .With(c => c.Phone, GenerateValidPhoneNumber())
                .With(c => c.Email, GenerateValidEmail())
                .Create();
        }

        private static string GenerateValidPhoneNumber()
        {
            // Example for generating a valid UK phone number
            var phoneNumber = fixture.Create<string>().Substring(0, 20);
            return phoneNumber;
        }

        private static string GenerateValidEmail()
        {
            var username = fixture.Create<string>().Substring(0, 8); // Limiting the username length
            var domain = fixture.Create<string>().Substring(0, 5); // Limiting the domain length
            return $"{username}@{domain}.com"; // Example: randomuser@domain.com
        }
    }
}
