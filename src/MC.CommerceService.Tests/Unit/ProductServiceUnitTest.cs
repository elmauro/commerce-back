using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.Products;
using MC.CommerceService.API.Services.v1.Queries.Product;
using MC.CommerceService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.ProductService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class ProductServiceUnitTest
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetProductByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddProductHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateProductHandler>> _loggerUpdateMock;
        private readonly GetProductByIdHandler _getHandler;
        private readonly AddProductHandler _addHandler;
        private readonly UpdateProductHandler _updateHandler;

        public ProductServiceUnitTest()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetProductByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddProductHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateProductHandler>>();
            _getHandler = new GetProductByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddProductHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateProductHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetProductByIdAsync_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetProductViewByIdAsync(productId.ToString()))
                .ReturnsAsync((ProductView?)null);

            // Act
            var result = await _getHandler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProductByIdAsync_ProductFound_DiscountApplied_ReturnsOk()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductView { ProductId = productId.ToString(), Price = 100 };
            _repositoryMock.Setup(repo => repo.GetProductViewByIdAsync(productId.ToString()))
                .ReturnsAsync(product);

            // Act
            var result = await _getHandler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<ProductView>>().Subject;
            var productResponse = response.Data;
        }

        [Fact]
        public async Task AddProductAsync_ValidProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var productRequest = new ProductRequest { Title = "Test Product" };
            var product = new Product { ProductId = Guid.NewGuid().ToString(), Title = "Test Product" };

            _mapperMock.Setup(mapper => mapper.Map<Product>(productRequest))
                .Returns(product);

            _repositoryMock.Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddProductCommand(productRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(ProductController.Create));
            createdAtActionResult?.RouteValues?["productId"].Should().Be(product.ProductId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<ProductRequest>>().Subject;
            response?.Data.Should().Be(productRequest);
        }

        [Fact]
        public async Task AddProductAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var productRequest = new ProductRequest { Title = "Test Product" };
            _mapperMock.Setup(mapper => mapper.Map<Product>(productRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddProductCommand(productRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateProductAsync_ExistingProduct_ReturnsNoContent()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRequest = new ProductRequest { Title = "Updated Product" };
            var existingProduct = new Product { ProductId = productId.ToString(), Title = "Existing Product" };
            var newProduct = new Product { Title = "Updated Product" };

            _repositoryMock.Setup(repo => repo.GetProductByIdAsync(productId.ToString()))
                .ReturnsAsync(existingProduct);

            _mapperMock.Setup(mapper => mapper.Map<Product>(productRequest))
                .Returns(newProduct);

            _mapperMock.Setup(mapper => mapper.Map(newProduct, existingProduct))
                .Returns(existingProduct);

            _repositoryMock.Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCommand(productId, productRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateProductAsync_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRequest = new ProductRequest { Title = "Updated Product" };

            _repositoryMock.Setup(repo => repo.GetProductByIdAsync(productId.ToString()))
                .ReturnsAsync((Product?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCommand(productId, productRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateProductAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productRequest = new ProductRequest { Title = "Updated Product" };

            _repositoryMock.Setup(repo => repo.GetProductByIdAsync(productId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateProductCommand(productId, productRequest), CancellationToken.None);


            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
