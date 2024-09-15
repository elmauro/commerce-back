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
    public class CategoryControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private const string CategoryRoute = "v1/Category";

        public CategoryControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCategoryReturnsNotFound()
        {
            // Arrange
            var categoryId = Guid.NewGuid().ToString();
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{CategoryRoute}/{categoryId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCategoryReturnsOk()
        {
            // Arrange
            var categoryToAdd = CategoryMockingData.GetCategory();
            categoryToAdd.ProductCategories = new List<ProductCategory>();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            dbContext.Categories.Add(categoryToAdd);
            await dbContext.SaveChangesAsync();

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{CategoryRoute}/{categoryToAdd.CategoryId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<CategoryView>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.CategoryName.Should().Be(categoryToAdd.CategoryName);

            dbContext.Categories.Remove(categoryToAdd);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task AddCategoryReturnsBadRequest()
        {
            // Arrange
            var categoryToAdd = CategoryMockingData.GetCategoryRequest();
            categoryToAdd.CategoryName = string.Empty;

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CategoryRoute}", categoryToAdd);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task AddCategoryReturnsOk()
        {
            // Arrange
            var categoryToAdd = CategoryMockingData.GetCategoryRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CategoryRoute}", categoryToAdd);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var categoryId = query.Get("categoryId");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Should().NotBeNull();

            var actionDataResponse = await response.Content.ReadFromJsonAsync<ActionDataResponse<CategoryRequest>>();
            actionDataResponse?.Data.Should().NotBeNull();
            actionDataResponse?.Data.CategoryName.Should().Be(categoryToAdd.CategoryName);

            var categoryToRemove = await dbContext.Categories.FindAsync(categoryId);
            dbContext.Categories.Remove(categoryToRemove);
            await dbContext.SaveChangesAsync();
        }

        [Fact]
        public async Task UpdateCategoryReturnsNotFound()
        {
            // Arrange
            var categoryId = Guid.NewGuid().ToString();
            var categoryToUpdate = CategoryMockingData.GetCategoryRequest();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync($"{CategoryRoute}/{categoryId}", categoryToUpdate);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCategoryReturnsOk()
        {
            // Arrange
            var categoryToUpdate = CategoryMockingData.GetCategoryRequest();

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CommerceDBContext>();

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{CategoryRoute}", categoryToUpdate);
            var query = System.Web.HttpUtility.ParseQueryString(response.Headers.Location.Query);
            var categoryId = query.Get("categoryId");

            var updateResponse = await client.PutAsJsonAsync($"{CategoryRoute}/{categoryId}", categoryToUpdate);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
            updateResponse.Should().NotBeNull();

            var existingCategory = await dbContext.Categories.FindAsync(categoryId);
            existingCategory?.CategoryName.Should().Be(categoryToUpdate.CategoryName);

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();
        }
    }
}
