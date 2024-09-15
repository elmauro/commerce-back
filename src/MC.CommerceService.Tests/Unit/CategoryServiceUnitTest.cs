using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.Categories;
using MC.CommerceService.API.Services.v1.Queries.Category;
using MC.CommerceService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.CategoryService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class CategoryServiceUnitTest
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetCategoryByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddCategoryHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateCategoryHandler>> _loggerUpdateMock;
        private readonly GetCategoryByIdHandler _getHandler;
        private readonly AddCategoryHandler _addHandler;
        private readonly UpdateCategoryHandler _updateHandler;

        public CategoryServiceUnitTest()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetCategoryByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddCategoryHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateCategoryHandler>>();
            _getHandler = new GetCategoryByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddCategoryHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateCategoryHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_CategoryNotFound_ReturnsNotFound()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetCategoryViewByIdAsync(categoryId.ToString()))
                .ReturnsAsync((CategoryView?)null);

            // Act
            var result = await _getHandler.Handle(new GetCategoryByIdQuery(categoryId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetCategoryByIdAsync_CategoryFound_ReturnsOk()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new CategoryView { CategoryId = categoryId.ToString(), CategoryName = "Test Category" };
            _repositoryMock.Setup(repo => repo.GetCategoryViewByIdAsync(categoryId.ToString()))
                .ReturnsAsync(category);

            // Act
            var result = await _getHandler.Handle(new GetCategoryByIdQuery(categoryId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<CategoryView>>().Subject;
            var categoryResponse = response.Data;
            categoryResponse.Should().BeEquivalentTo(category);
        }

        [Fact]
        public async Task AddCategoryAsync_ValidCategory_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var categoryRequest = new CategoryRequest { CategoryName = "New Category" };
            var category = new Category { CategoryId = Guid.NewGuid().ToString(), CategoryName = "New Category" };

            _mapperMock.Setup(mapper => mapper.Map<Category>(categoryRequest))
                .Returns(category);

            _repositoryMock.Setup(repo => repo.AddCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddCategoryCommand(categoryRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(CategoryController.Create));
            createdAtActionResult?.RouteValues?["categoryId"].Should().Be(category.CategoryId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<CategoryRequest>>().Subject;
            response?.Data.Should().Be(categoryRequest);
        }

        [Fact]
        public async Task AddCategoryAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var categoryRequest = new CategoryRequest { CategoryName = "New Category" };
            _mapperMock.Setup(mapper => mapper.Map<Category>(categoryRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddCategoryCommand(categoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ExistingCategory_ReturnsNoContent()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var categoryRequest = new CategoryRequest { CategoryName = "Updated Category" };
            var existingCategory = new Category { CategoryId = categoryId.ToString(), CategoryName = "Existing Category" };
            var newCategory = new Category { CategoryName = "Updated Category" };

            _repositoryMock.Setup(repo => repo.GetCategoryByIdAsync(categoryId.ToString()))
                .ReturnsAsync(existingCategory);

            _mapperMock.Setup(mapper => mapper.Map<Category>(categoryRequest))
                .Returns(newCategory);

            _mapperMock.Setup(mapper => mapper.Map(newCategory, existingCategory))
                .Returns(existingCategory);

            _repositoryMock.Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateCategoryCommand(categoryId, categoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateCategoryAsync_CategoryNotFound_ReturnsNotFound()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var categoryRequest = new CategoryRequest { CategoryName = "Updated Category" };

            _repositoryMock.Setup(repo => repo.GetCategoryByIdAsync(categoryId.ToString()))
                .ReturnsAsync((Category?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateCategoryCommand(categoryId, categoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateCategoryAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var categoryRequest = new CategoryRequest { CategoryName = "Updated Category" };

            _repositoryMock.Setup(repo => repo.GetCategoryByIdAsync(categoryId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateCategoryCommand(categoryId, categoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
