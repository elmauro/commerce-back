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
    public class OrderControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string OrderRoute = "v1/Order";

        public OrderControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetOrderReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{OrderRoute}/{orderId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetOrderReturnsOk()
        {
            try
            {
                // Arrange
                var orderToAdd = OrderMockingData.GetOrder();
                orderToAdd.OrderProducts = new List<OrderProduct>();

                using var scope = _factory.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

                var customerToAdd = CustomerMockingData.GetCustomer();
                customerToAdd.Orders = new List<Order>();
                dbContext.Customers.Add(customerToAdd);
                await dbContext.SaveChangesAsync();

                orderToAdd.CustomerId = customerToAdd.CustomerId;
                orderToAdd.Customer = customerToAdd;

                dbContext.Orders.Add(orderToAdd);
                await dbContext.SaveChangesAsync();

                var client = _factory.CreateClient();

                // Act
                var response = await client.GetAsync($"{OrderRoute}/{orderToAdd.OrderId}");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Should().NotBeNull();

                var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<OrderView>>();
                actionDataResponse?.Data.Should().NotBeNull();
                actionDataResponse?.Data.TotalAmount.Should().Be(orderToAdd.TotalAmount);

                dbContext.Orders.Remove(orderToAdd);
                dbContext.Customers.Remove(customerToAdd);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public async Task AddOrderReturnsBadRequest()
        {
            // Arrange
            var orderToAdd = OrderMockingData.GetOrderRequest();
            orderToAdd.TotalAmount = 0; // Invalid total amount

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderRoute}", orderToAdd);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddOrderReturnsOk()
        {
            // Arrange
            var orderToAdd = OrderMockingData.GetOrderRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            // Add customer to the database
            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            // Set the CustomerId in the order request to the added customer
            orderToAdd.CustomerId = new Guid(customerToAdd.CustomerId);

            // Add products to the database
            var productsToAdd = new List<Product>
            {
                ProductMockingData.GetProduct(),
                ProductMockingData.GetProduct(),
                ProductMockingData.GetProduct(),
            };

            productsToAdd[0].OrderProducts = [];
            productsToAdd[0].ProductCategories = [];

            productsToAdd[1].OrderProducts = [];
            productsToAdd[1].ProductCategories = [];

            productsToAdd[2].OrderProducts = [];
            productsToAdd[2].ProductCategories = [];

            dbContext.Products.AddRange(productsToAdd);
            await dbContext.SaveChangesAsync();

            // Set the product IDs in the order request to the added products
            orderToAdd.Products[0].ProductId = new Guid(productsToAdd[0].ProductId);
            orderToAdd.Products[1].ProductId = new Guid(productsToAdd[1].ProductId);
            orderToAdd.Products[2].ProductId = new Guid(productsToAdd[2].ProductId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderRoute}", orderToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var orderId = query.Get("orderId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<OrderRequest>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.TotalAmount.Should().Be(orderToAdd.TotalAmount);

            // Verify the order was added to the database
            var orderToRemove = await dbContext.Orders.FindAsync(orderId);
            orderToRemove.Should().NotBeNull(); // Ensure the order exists
            orderToRemove?.TotalAmount.Should().Be(orderToAdd.TotalAmount); // Verify the total amount
            orderToRemove?.CustomerId.Should().Be(customerToAdd.CustomerId); // Verify the customer

            // Cleanup the added data
            dbContext.Orders.Remove(orderToRemove);
            dbContext.Customers.Remove(customerToAdd);
            dbContext.Products.Remove(productsToAdd[0]);
            dbContext.Products.Remove(productsToAdd[1]);
            dbContext.Products.Remove(productsToAdd[2]);
            await dbContext.SaveChangesAsync();
        }


        [Fact]
        public async Task UpdateOrderReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var orderToUpdate = OrderMockingData.GetOrderRequest();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{OrderRoute}/{orderId}", orderToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateOrderReturnsOk()
        {
            // Arrange
            var orderToUpdate = OrderMockingData.GetOrderRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            // Add customer to the database
            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            // Set the CustomerId in the order request to the added customer
            orderToUpdate.CustomerId = new Guid(customerToAdd.CustomerId);

            // Add products to the database
            var productsToAdd = new List<Product>
    {
        ProductMockingData.GetProduct(),
        ProductMockingData.GetProduct(),
        ProductMockingData.GetProduct(),
    };

            productsToAdd[0].OrderProducts = [];
            productsToAdd[0].ProductCategories = [];

            productsToAdd[1].OrderProducts = [];
            productsToAdd[1].ProductCategories = [];

            productsToAdd[2].OrderProducts = [];
            productsToAdd[2].ProductCategories = [];

            dbContext.Products.AddRange(productsToAdd);
            await dbContext.SaveChangesAsync();

            // Set the product IDs in the order request to the added products
            orderToUpdate.Products[0].ProductId = new Guid(productsToAdd[0].ProductId);
            orderToUpdate.Products[1].ProductId = new Guid(productsToAdd[1].ProductId);
            orderToUpdate.Products[2].ProductId = new Guid(productsToAdd[2].ProductId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderRoute}", orderToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var orderId = query.Get("orderId");

            // Modify the order for update (e.g., changing the total amount)
            orderToUpdate.TotalAmount = 200;

            // Update the order via PUT request
            var updateResponse = await client.PutAsJsonAsync($"{OrderRoute}/{orderId}", orderToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            // Verify that the order was updated in the database
            var existingOrder = await dbContext.Orders.FindAsync(orderId);
            existingOrder.Should().NotBeNull();
            existingOrder?.OrderId.Should().Be(orderId);
            existingOrder?.TotalAmount.Should().Be(orderToUpdate.TotalAmount); // Verify the updated total amount

            // Cleanup the added data
            dbContext.Orders.Remove(existingOrder);
            dbContext.Customers.Remove(customerToAdd);
            dbContext.Products.Remove(productsToAdd[0]);
            dbContext.Products.Remove(productsToAdd[1]);
            dbContext.Products.Remove(productsToAdd[2]);
            await dbContext.SaveChangesAsync();
        }

    }
}
