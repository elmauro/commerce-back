using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data;
using MC.CommerceService.API.Options;
using MC.CommerceService.Tests.Fixtures;
using MC.Insurance.ApplicationServicesTest.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace MC.CommerceService.Tests.Integration.v1
{
    public class ProductCategoryControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string ProductCategoryRoute = "v1/ProductCategory";

        public ProductCategoryControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetProductCategoryReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{ProductCategoryRoute}/{productId}/{categoryId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetProductCategoryReturnsOk()
        {
            // Arrange
            var productCategoryToAdd = ProductCategoryMockingData.GetProductCategory();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.ProductCategories = [];
            productToAdd.OrderProducts = [];

            var categoryToAdd = CategoryMockingData.GetCategory();
            categoryToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            dbContext.Categories.Add(categoryToAdd);
            await dbContext.SaveChangesAsync();

            productCategoryToAdd.ProductId = productToAdd.ProductId;
            productCategoryToAdd.Product = productToAdd;

            productCategoryToAdd.CategoryId = categoryToAdd.CategoryId;
            productCategoryToAdd.Category = categoryToAdd;

            dbContext.ProductCategory.Add(productCategoryToAdd);
            await dbContext.SaveChangesAsync();

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{ProductCategoryRoute}/{productCategoryToAdd.ProductId}/{productCategoryToAdd.CategoryId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<ProductCategoryView>>();
            actionDataResponse?.Data.Should().NotBeNull();

            dbContext.Products.Remove(productToAdd);
            dbContext.Categories.Remove(categoryToAdd);
            dbContext.ProductCategory.Remove(productCategoryToAdd);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddProductCategoryReturnsBadRequest()
        {
            // Arrange
            var productCategoryToAdd = ProductCategoryMockingData.GetProductCategoryRequest();
            productCategoryToAdd.ProductId = Guid.Empty; // Invalid product ID

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{ProductCategoryRoute}", productCategoryToAdd);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddProductCategoryReturnsOk()
        {
            // Arrange
            var productCategoryToAdd = ProductCategoryMockingData.GetProductCategoryRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.ProductCategories = [];
            productToAdd.OrderProducts = [];

            var categoryToAdd = CategoryMockingData.GetCategory();
            categoryToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            dbContext.Categories.Add(categoryToAdd);
            await dbContext.SaveChangesAsync();

            productCategoryToAdd.ProductId = new Guid(productToAdd.ProductId);
            productCategoryToAdd.CategoryId = new Guid(categoryToAdd.CategoryId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{ProductCategoryRoute}", productCategoryToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var productId = query.Get("productId");
            var categoryId = query.Get("categoryId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<ProductCategoryRequest>>();
            actionDataResponse?.Data.Should().NotBeNull();

            var productCategoryToRemove = await dbContext.ProductCategory.FindAsync(productId, categoryId);
            dbContext.Products.Remove(productToAdd);
            dbContext.Categories.Remove(categoryToAdd);
            dbContext.ProductCategory.Remove(productCategoryToRemove);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateProductCategoryReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            var productCategoryToUpdate = ProductCategoryMockingData.GetProductCategoryRequest();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{ProductCategoryRoute}/{productId}/{categoryId}", productCategoryToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateProductCategoryReturnsBadRequest()
        {
            // Arrange
            var productId = Guid.NewGuid().ToString();
            var categoryId = Guid.NewGuid().ToString();
            var productCategoryToUpdate = ProductCategoryMockingData.GetProductCategoryRequest();
            productCategoryToUpdate.ProductId = Guid.Empty; // Invalid ProductId

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{ProductCategoryRoute}/{productId}/{categoryId}", productCategoryToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateProductCategoryReturnsOk()
        {
            // Arrange
            var productCategoryToUpdate = ProductCategoryMockingData.GetProductCategoryRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var productToAdd = ProductMockingData.GetProduct();
            productToAdd.ProductCategories = [];
            productToAdd.OrderProducts = [];

            var categoryToAdd = CategoryMockingData.GetCategory();
            categoryToAdd.ProductCategories = [];

            dbContext.Products.Add(productToAdd);
            await dbContext.SaveChangesAsync();

            dbContext.Categories.Add(categoryToAdd);
            await dbContext.SaveChangesAsync();

            productCategoryToUpdate.ProductId = new Guid(productToAdd.ProductId);
            productCategoryToUpdate.CategoryId = new Guid(categoryToAdd.CategoryId);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{ProductCategoryRoute}", productCategoryToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var productId = query.Get("productId");
            var categoryId = productCategoryToUpdate.CategoryId.ToString();

            var updateResponse = await client.PutAsJsonAsync($"{ProductCategoryRoute}/{productId}/{categoryId}", productCategoryToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingProductCategory = await dbContext.ProductCategory.FindAsync(productId, categoryId);

            existingProductCategory?.ProductId.Should().Be(productId);
            existingProductCategory?.CategoryId.Should().Be(categoryId);

            dbContext.Products.Remove(productToAdd);
            dbContext.Categories.Remove(categoryToAdd);
            dbContext.ProductCategory.Remove(existingProductCategory);
            await dbContext.SaveChangesAsync();
        }
    }
}
