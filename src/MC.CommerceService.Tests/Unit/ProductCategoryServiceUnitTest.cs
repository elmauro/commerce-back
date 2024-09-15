using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.ProductCategories;
using MC.CommerceService.API.Services.v1.Queries.ProductCategory;
using MC.CommerceService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.ProductCategoryService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class ProductCategoryServiceUnitTest
    {
        private readonly Mock<IProductCategoryRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetProductCategoryByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddProductCategoryHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateProductCategoryHandler>> _loggerUpdateMock;
        private readonly GetProductCategoryByIdHandler _getHandler;
        private readonly AddProductCategoryHandler _addHandler;
        private readonly UpdateProductCategoryHandler _updateHandler;

        public ProductCategoryServiceUnitTest()
        {
            _repositoryMock = new Mock<IProductCategoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetProductCategoryByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddProductCategoryHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateProductCategoryHandler>>();
            _getHandler = new GetProductCategoryByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddProductCategoryHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateProductCategoryHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetProductCategoryByIdAsync_ProductCategoryNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetProductCategoryViewByIdAsync(productId.ToString(), categoryId.ToString()))
                .ReturnsAsync((ProductCategoryView?)null);

            // Act
            var result = await _getHandler.Handle(new GetProductCategoryByIdQuery(productId, categoryId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProductCategoryByIdAsync_ProductCategoryFound_ReturnsOk()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var productCategory = new ProductCategoryView { ProductId = productId.ToString(), CategoryId = categoryId.ToString() };
            _repositoryMock.Setup(repo => repo.GetProductCategoryViewByIdAsync(productId.ToString(), categoryId.ToString()))
                .ReturnsAsync(productCategory);

            // Act
            var result = await _getHandler.Handle(new GetProductCategoryByIdQuery(productId, categoryId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<ProductCategoryView>>().Subject;
            var productCategoryResponse = response.Data;
            productCategoryResponse.Should().BeEquivalentTo(productCategory);
        }

        [Fact]
        public async Task AddProductCategoryAsync_ValidProductCategory_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var productCategoryRequest = new ProductCategoryRequest { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() };
            var productCategory = new ProductCategory { ProductId = productCategoryRequest.ProductId.ToString(), CategoryId = productCategoryRequest.CategoryId.ToString() };

            _mapperMock.Setup(mapper => mapper.Map<ProductCategory>(productCategoryRequest))
                .Returns(productCategory);

            _repositoryMock.Setup(repo => repo.AddProductCategoryAsync(It.IsAny<ProductCategory>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddProductCategoryCommand(productCategoryRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(ProductCategoryController.Create));
            createdAtActionResult?.RouteValues?["productId"].Should().Be(productCategory.ProductId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<ProductCategoryRequest>>().Subject;
            response?.Data.Should().Be(productCategoryRequest);
        }

        [Fact]
        public async Task AddProductCategoryAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var productCategoryRequest = new ProductCategoryRequest { ProductId = Guid.NewGuid(), CategoryId = Guid.NewGuid() };
            _mapperMock.Setup(mapper => mapper.Map<ProductCategory>(productCategoryRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddProductCategoryCommand(productCategoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_ExistingProductCategory_ReturnsNoContent()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var productCategoryRequest = new ProductCategoryRequest { ProductId = productId, CategoryId = categoryId };
            var existingProductCategory = new ProductCategory { ProductId = productId.ToString(), CategoryId = categoryId.ToString() };
            var newProductCategory = new ProductCategory { ProductId = productId.ToString(), CategoryId = categoryId.ToString() };

            _repositoryMock.Setup(repo => repo.GetProductCategoryAsync(productId.ToString(), categoryId.ToString()))
                .ReturnsAsync(existingProductCategory);

            _mapperMock.Setup(mapper => mapper.Map<ProductCategory>(productCategoryRequest))
                .Returns(newProductCategory);

            _mapperMock.Setup(mapper => mapper.Map(newProductCategory, existingProductCategory))
                .Returns(existingProductCategory);

            _repositoryMock.Setup(repo => repo.UpdateProductCategoryAsync(It.IsAny<ProductCategory>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCategoryCommand(productId, categoryId, productCategoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_ProductCategoryNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var productCategoryRequest = new ProductCategoryRequest { ProductId = productId, CategoryId = categoryId };

            _repositoryMock.Setup(repo => repo.GetProductCategoryAsync(productId.ToString(), categoryId.ToString()))
                .ReturnsAsync((ProductCategory?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCategoryCommand(productId, categoryId, productCategoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var productCategoryRequest = new ProductCategoryRequest { ProductId = productId, CategoryId = categoryId };

            _repositoryMock.Setup(repo => repo.GetProductCategoryAsync(productId.ToString(), categoryId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCategoryCommand(productId, categoryId, productCategoryRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
