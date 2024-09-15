using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.Orders;
using MC.CommerceService.API.Services.v1.Queries.Order;
using MC.CommerceService.Tests.Fixtures;
using MC.Insurance.ApplicationServicesTest.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.OrderService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class OrderServiceUnitTest
    {
        private readonly Mock<IOrderRepository> _repositoryMock;
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IProductRepository> _productRepository;

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetOrderByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddOrderHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateOrderHandler>> _loggerUpdateMock;
        private readonly GetOrderByIdHandler _getHandler;
        private readonly AddOrderHandler _addHandler;
        private readonly UpdateOrderHandler _updateHandler;

        public OrderServiceUnitTest()
        {
            _repositoryMock = new Mock<IOrderRepository>();
            _orderRepository = new Mock<IOrderRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
            _productRepository = new Mock<IProductRepository>();

            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetOrderByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddOrderHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateOrderHandler>>();
            _getHandler = new GetOrderByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddOrderHandler(
                _orderRepository.Object,
                _customerRepository.Object,
                _productRepository.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateOrderHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetOrderByIdAsync_OrderNotFound_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetOrderViewByIdAsync(orderId.ToString()))
                .ReturnsAsync((OrderView?)null);

            // Act
            var result = await _getHandler.Handle(new GetOrderByIdQuery(orderId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetOrderByIdAsync_OrderFound_ReturnsOk()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new OrderView { OrderId = orderId.ToString(), TotalAmount = 150 };
            _repositoryMock.Setup(repo => repo.GetOrderViewByIdAsync(orderId.ToString()))
                .ReturnsAsync(order);

            // Act
            var result = await _getHandler.Handle(new GetOrderByIdQuery(orderId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<OrderView>>().Subject;
            var orderResponse = response.Data;
            orderResponse.Should().BeEquivalentTo(order);
        }

        [Fact]
        public async Task AddOrderAsync_ValidOrder_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                TotalAmount = 150,
                CustomerId = Guid.NewGuid(),
                Products =
                [
                    new() { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new() { ProductId = Guid.NewGuid(), Quantity = 1 }
                ]
            };

            var products = new List<Product>() { 
                ProductMockingData.GetProduct(),
                ProductMockingData.GetProduct()
            };

            products[0].ProductId = orderRequest.Products[0].ProductId.ToString();
            products[1].ProductId = orderRequest.Products[1].ProductId.ToString();


            var customer = CustomerMockingData.GetCustomer();
            customer.CustomerId = orderRequest.CustomerId.ToString();

            var order = new Order { OrderId = Guid.NewGuid().ToString(), TotalAmount = 150 };

            _mapperMock.Setup(mapper => mapper.Map<Order>(orderRequest))
                .Returns(order);

            _customerRepository.Setup(repo => repo.GetCustomerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(customer);

            _productRepository.Setup(repo => repo.GetProductsByIdsAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(products);

            _repositoryMock.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>(), It.IsAny<List<OrderProduct>>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddOrderCommand(orderRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(OrderController.Create));
            createdAtActionResult?.RouteValues?["orderId"].Should().Be(order.OrderId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<OrderRequest>>().Subject;
            response?.Data.Should().Be(orderRequest);

            // Verify that the product repository was called with the correct product IDs
            _productRepository.Verify(repo => repo.GetProductsByIdsAsync(
                It.Is<IEnumerable<string>>(ids => ids.SequenceEqual(orderRequest.Products.Select(p => p.ProductId.ToString())))),
                Times.Once);

            // Verify that the order was saved
            _orderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<Order>(), It.IsAny<List<OrderProduct>>()), Times.Once);
        }

        [Fact]
        public async Task AddOrderAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                TotalAmount = 150,
                CustomerId = Guid.NewGuid(),
                Products = new List<ProductQuantityRequest>
                {
                    new OrderProductRequest { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new OrderProductRequest { ProductId = Guid.NewGuid(), Quantity = 1 }
                }
            };

            var customer = CustomerMockingData.GetCustomer();
            customer.CustomerId = orderRequest.CustomerId.ToString();

            var products = new List<Product>()
            {
                ProductMockingData.GetProduct(),
                ProductMockingData.GetProduct()
            };

            products[0].ProductId = orderRequest.Products[0].ProductId.ToString();
            products[1].ProductId = orderRequest.Products[1].ProductId.ToString();

            // Mock repository to return valid customer and products
            _customerRepository.Setup(repo => repo.GetCustomerByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(customer);

            _productRepository.Setup(repo => repo.GetProductsByIdsAsync(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(products);

            _mapperMock.Setup(mapper => mapper.Map<Order>(orderRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddOrderCommand(orderRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateOrderAsync_ExistingOrder_ReturnsNoContent()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderRequest = new OrderRequest { TotalAmount = 200, CustomerId = Guid.NewGuid() };
            var existingOrder = new Order { OrderId = orderId.ToString(), TotalAmount = 150 };
            var newOrder = new Order { TotalAmount = 200 };

            _repositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId.ToString()))
                .ReturnsAsync(existingOrder);

            _mapperMock.Setup(mapper => mapper.Map<Order>(orderRequest))
                .Returns(newOrder);

            _mapperMock.Setup(mapper => mapper.Map(newOrder, existingOrder))
                .Returns(existingOrder);

            _repositoryMock.Setup(repo => repo.UpdateOrderAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderCommand(orderId, orderRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateOrderAsync_OrderNotFound_ReturnsNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderRequest = new OrderRequest { TotalAmount = 200, CustomerId = Guid.NewGuid() };

            _repositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId.ToString()))
                .ReturnsAsync((Order?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderCommand(orderId, orderRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateOrderAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var orderRequest = new OrderRequest { TotalAmount = 200, CustomerId = Guid.NewGuid() };

            _repositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateOrderCommand(orderId, orderRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
