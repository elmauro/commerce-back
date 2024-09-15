using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Options;
using MC.Insurance.ApplicationServicesTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace MC.CommerceService.Tests.Integration.v1
{
    public class CustomerControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string CustomerRoute = "v1/Customer";

        public CustomerControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCustomerReturnsNotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{CustomerRoute}/{customerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCustomerReturnsOk()
        {
            // Arrange
            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{CustomerRoute}/{customerToAdd.CustomerId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<CustomerView>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.FirstName.Should().Be(customerToAdd.FirstName);

            dbContext.Customers.Remove(customerToAdd);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddCustomerReturnsBadRequest()
        {
            // Arrange
            var customerToAdd = CustomerMockingData.GetCustomerRequest();
            customerToAdd.FirstName = string.Empty;

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CustomerRoute}", customerToAdd);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddCustomerReturnsOk()
        {
            // Arrange
            var customerToAdd = CustomerMockingData.GetCustomerRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CustomerRoute}", customerToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var customerId = query.Get("customerId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<CustomerRequest>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.FirstName.Should().Be(customerToAdd.FirstName);

            var customerToRemove = await dbContext.Customers.FindAsync(customerId);
            dbContext.Customers.Remove(customerToRemove);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateCustomerReturnsNotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid().ToString();
            var customerToUpdate = CustomerMockingData.GetCustomerRequest();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{CustomerRoute}/{customerId}", customerToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCustomerReturnsOk()
        {
            // Arrange
            var customerToUpdate = CustomerMockingData.GetCustomerRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CustomerRoute}", customerToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var customerId = query.Get("customerId");

            var updateResponse = await client.PutAsJsonAsync($"{CustomerRoute}/{customerId}", customerToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingCustomer = await dbContext.Customers.FindAsync(customerId);

            existingCustomer?.CustomerId.Should().Be(customerId);
            existingCustomer?.FirstName.Should().Be(customerToUpdate.FirstName);
            existingCustomer?.LastName.Should().Be(customerToUpdate.LastName);
            existingCustomer?.Email.Should().Be(customerToUpdate.Email);
            existingCustomer?.Phone.Should().Be(customerToUpdate.Phone);

            dbContext.Customers.Remove(existingCustomer);
            await dbContext.SaveChangesAsync();
        }
    }
}
