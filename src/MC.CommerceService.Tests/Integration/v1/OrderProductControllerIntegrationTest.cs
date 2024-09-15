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
    public class OrderProductControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string OrderProductRoute = "v1/OrderProduct";

        public OrderProductControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetOrderProductReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var productId = Guid.NewGuid().ToString();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{OrderProductRoute}/{orderId}/{productId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetOrderProductReturnsOk()
        {
            // Arrange
            var orderProductToAdd = OrderProductMockingData.GetOrderProduct();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var orderToAdd = OrderMockingData.GetOrder();
            orderToAdd.OrderProducts = new List<OrderProduct>();

            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            orderToAdd.CustomerId = customerToAdd.CustomerId;
            orderToAdd.Customer = customerToAdd;

            dbContext.Orders.Add(orderToAdd);
            await dbContext.SaveChangesAsync();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.OrderProducts = [];
            productToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            orderProductToAdd.OrderId = orderToAdd.OrderId;
            orderProductToAdd.Order = orderToAdd;

            orderProductToAdd.ProductId = productToAdd.ProductId;
            orderProductToAdd.Product = productToAdd;

            dbContext.OrderProducts.Add(orderProductToAdd);
            await dbContext.SaveChangesAsync();

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{OrderProductRoute}/{orderProductToAdd.OrderId}/{orderProductToAdd.ProductId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<OrderProductView>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.Quantity.Should().Be(orderProductToAdd.Quantity);

            dbContext.Customers.Remove(customerToAdd);
            dbContext.Orders.Remove(orderToAdd);
            dbContext.Products.Remove(productToAdd);
            dbContext.OrderProducts.Remove(orderProductToAdd);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddOrderProductReturnsBadRequest()
        {
            // Arrange
            var orderProductToAdd = OrderProductMockingData.GetOrderProductRequest();
            orderProductToAdd.Quantity = 0; // Invalid quantity

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderProductRoute}", orderProductToAdd);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddOrderProductReturnsOk()
        {
            // Arrange
            var orderProductToAdd = OrderProductMockingData.GetOrderProductRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var orderToAdd = OrderMockingData.GetOrder();
            orderToAdd.OrderProducts = new List<OrderProduct>();

            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            orderToAdd.CustomerId = customerToAdd.CustomerId;
            orderToAdd.Customer = customerToAdd;

            dbContext.Orders.Add(orderToAdd);
            await dbContext.SaveChangesAsync();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.OrderProducts = [];
            productToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            orderProductToAdd.OrderId = new Guid(orderToAdd.OrderId);
            orderProductToAdd.ProductId = new Guid(productToAdd.ProductId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderProductRoute}", orderProductToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var orderId = query.Get("orderId");
            var productId = query.Get("productId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<OrderProductRequest>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.Quantity.Should().Be(orderProductToAdd.Quantity);

            var orderProductToRemove = await dbContext.OrderProducts.FindAsync(orderId, productId);
            dbContext.Customers.Remove(customerToAdd);
            dbContext.Orders.Remove(orderToAdd);
            dbContext.Products.Remove(productToAdd);
            dbContext.OrderProducts.Remove(orderProductToRemove);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateOrderProductReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var productId = Guid.NewGuid().ToString();
            var orderProductToUpdate = OrderProductMockingData.GetOrderProductRequest();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{OrderProductRoute}/{orderId}/{productId}", orderProductToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateOrderProductReturnsBadRequest()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var productId = Guid.NewGuid().ToString();
            var orderProductToUpdate = OrderProductMockingData.GetOrderProductRequest();
            orderProductToUpdate.Quantity = 0; // Invalid quantity

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{OrderProductRoute}/{orderId}/{productId}", orderProductToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateOrderProductReturnsOk()
        {
            // Arrange
            var orderProductToUpdate = OrderProductMockingData.GetOrderProductRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var orderToAdd = OrderMockingData.GetOrder();
            orderToAdd.OrderProducts = new List<OrderProduct>();

            var customerToAdd = CustomerMockingData.GetCustomer();
            customerToAdd.Orders = new List<Order>();
            dbContext.Customers.Add(customerToAdd);
            await dbContext.SaveChangesAsync();

            orderToAdd.CustomerId = customerToAdd.CustomerId;
            orderToAdd.Customer = customerToAdd;

            dbContext.Orders.Add(orderToAdd);
            await dbContext.SaveChangesAsync();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.OrderProducts = [];
            productToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            orderProductToUpdate.OrderId = new Guid(orderToAdd.OrderId);

            orderProductToUpdate.ProductId = new Guid(productToAdd.ProductId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{OrderProductRoute}", orderProductToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var orderId = query.Get("orderId");
            var productId = orderProductToUpdate.ProductId.ToString();

            var updateResponse = await client.PutAsJsonAsync($"{OrderProductRoute}/{orderId}/{productId}", orderProductToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingOrderProduct = await dbContext.OrderProducts.FindAsync(orderId, productId);

            existingOrderProduct?.OrderId.Should().Be(orderId);
            existingOrderProduct?.ProductId.Should().Be(productId);
            existingOrderProduct?.Quantity.Should().Be(orderProductToUpdate.Quantity);

            dbContext.Customers.Remove(customerToAdd);
            dbContext.Orders.Remove(orderToAdd);
            dbContext.Products.Remove(productToAdd);
            dbContext.OrderProducts.Remove(existingOrderProduct);
            await dbContext.SaveChangesAsync();
        }
    }
}


