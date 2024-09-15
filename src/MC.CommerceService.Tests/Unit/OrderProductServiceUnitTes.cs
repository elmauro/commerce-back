using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.OrderProducts;
using MC.CommerceService.API.Services.v1.Queries.OrderProduct;
using MC.CommerceService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.OrderProductService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class OrderProductServiceUnitTest
    {
        private readonly Mock<IOrderProductRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetOrderProductByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddOrderProductHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateOrderProductHandler>> _loggerUpdateMock;
        private readonly GetOrderProductByIdHandler _getHandler;
        private readonly AddOrderProductHandler _addHandler;
        private readonly UpdateOrderProductHandler _updateHandler;

        public OrderProductServiceUnitTest()
        {
            _repositoryMock = new Mock<IOrderProductRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetOrderProductByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddOrderProductHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateOrderProductHandler>>();
            _getHandler = new GetOrderProductByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddOrderProductHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateOrderProductHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetOrderProductByIdAsync_OrderProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetOrderProductViewByIdAsync(orderId.ToString(), productId.ToString()))
                .ReturnsAsync((OrderProductView?)null);

            // Act
            var result = await _getHandler.Handle(new GetOrderProductByIdQuery(orderId, productId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetOrderProductByIdAsync_OrderProductFound_ReturnsOk()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var orderProduct = new OrderProductView { OrderId = orderId.ToString(), ProductId = productId.ToString(), Quantity = 2 };
            _repositoryMock.Setup(repo => repo.GetOrderProductViewByIdAsync(orderId.ToString(), productId.ToString()))
                .ReturnsAsync(orderProduct);

            // Act
            var result = await _getHandler.Handle(new GetOrderProductByIdQuery(orderId, productId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<OrderProductView>>().Subject;
            var orderProductResponse = response.Data;
            orderProductResponse.Should().BeEquivalentTo(orderProduct);
        }

        [Fact]
        public async Task AddOrderProductAsync_ValidOrderProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var orderProductRequest = new OrderProductRequest { ProductId = Guid.NewGuid(), Quantity = 2 };
            var orderProduct = new OrderProduct { OrderId = Guid.NewGuid().ToString(), ProductId = orderProductRequest.ProductId.ToString(), Quantity = 2 };

            _mapperMock.Setup(mapper => mapper.Map<OrderProduct>(orderProductRequest))
                .Returns(orderProduct);

            _repositoryMock.Setup(repo => repo.AddOrderProductAsync(It.IsAny<OrderProduct>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddOrderProductCommand(orderProductRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(OrderProductController.Create));
            createdAtActionResult?.RouteValues?["orderId"].Should().Be(orderProduct.OrderId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<OrderProductRequest>>().Subject;
            response?.Data.Should().Be(orderProductRequest);
        }

        [Fact]
        public async Task AddOrderProductAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var orderProductRequest = new OrderProductRequest { ProductId = Guid.NewGuid(), Quantity = 2 };
            _mapperMock.Setup(mapper => mapper.Map<OrderProduct>(orderProductRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddOrderProductCommand(orderProductRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateOrderProductAsync_ExistingOrderProduct_ReturnsNoContent()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var orderProductRequest = new OrderProductRequest { ProductId = productId, Quantity = 3 };
            var existingOrderProduct = new OrderProduct { OrderId = orderId.ToString(), ProductId = productId.ToString(), Quantity = 2 };
            var newOrderProduct = new OrderProduct { Quantity = 3 };

            _repositoryMock.Setup(repo => repo.GetOrderProductAsync(orderId.ToString(), productId.ToString()))
                .ReturnsAsync(existingOrderProduct);

            _mapperMock.Setup(mapper => mapper.Map<OrderProduct>(orderProductRequest))
                .Returns(newOrderProduct);

            _mapperMock.Setup(mapper => mapper.Map(newOrderProduct, existingOrderProduct))
                .Returns(existingOrderProduct);

            _repositoryMock.Setup(repo => repo.UpdateOrderProductAsync(It.IsAny<OrderProduct>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderProductCommand(orderId, productId, orderProductRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateOrderProductAsync_OrderProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var orderProductRequest = new OrderProductRequest { ProductId = productId, Quantity = 3 };

            _repositoryMock.Setup(repo => repo.GetOrderProductAsync(orderId.ToString(), productId.ToString()))
                .ReturnsAsync((OrderProduct?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderProductCommand(orderId, productId, orderProductRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateOrderProductAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var orderProductRequest = new OrderProductRequest { ProductId = productId, Quantity = 3 };

            _repositoryMock.Setup(repo => repo.GetOrderProductAsync(orderId.ToString(), productId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderProductCommand(orderId, productId, orderProductRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
