using AutoFixture;
using AutoMapper;
using FluentAssertions;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands.Customers;
using MC.CommerceService.API.Services.v1.Queries.Customer;
using MC.CommerceService.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MC.CustomerService.Tests.Unit
{
    [Collection(TestCollections.Integration)]
    public class CustomerServiceUnitTest
    {
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<GetCustomerByIdHandler>> _loggerGetMock;
        private readonly Mock<ILogger<AddCustomerHandler>> _loggerAddMock;
        private readonly Mock<ILogger<UpdateCustomerHandler>> _loggerUpdateMock;
        private readonly GetCustomerByIdHandler _getHandler;
        private readonly AddCustomerHandler _addHandler;
        private readonly UpdateCustomerHandler _updateHandler;

        public CustomerServiceUnitTest()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerGetMock = new Mock<ILogger<GetCustomerByIdHandler>>();
            _loggerAddMock = new Mock<ILogger<AddCustomerHandler>>();
            _loggerUpdateMock = new Mock<ILogger<UpdateCustomerHandler>>();
            _getHandler = new GetCustomerByIdHandler(
                _repositoryMock.Object,
                _loggerGetMock.Object);
            _addHandler = new AddCustomerHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerAddMock.Object);
            _updateHandler = new UpdateCustomerHandler(
                _repositoryMock.Object,
                _mapperMock.Object,
                _loggerUpdateMock.Object);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_CustomerNotFound_ReturnsNotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetCustomerViewByIdAsync(customerId.ToString()))
                .ReturnsAsync((CustomerView?)null);

            // Act
            var result = await _getHandler.Handle(new GetCustomerByIdQuery(customerId), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetCustomerByIdAsync_CustomerFound_ReturnsOk()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new CustomerView { CustomerId = customerId.ToString(), FirstName = "John", LastName = "Doe" };
            _repositoryMock.Setup(repo => repo.GetCustomerViewByIdAsync(customerId.ToString()))
                .ReturnsAsync(customer);

            // Act
            var result = await _getHandler.Handle(new GetCustomerByIdQuery(customerId), CancellationToken.None);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var response = okResult.Value.Should().BeOfType<ActionDataResponse<CustomerView>>().Subject;
            var customerResponse = response.Data;
            customerResponse.Should().BeEquivalentTo(customer);
        }

        [Fact]
        public async Task AddCustomerAsync_ValidCustomer_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var customerRequest = new CustomerRequest { FirstName = "John", LastName = "Doe" };
            var customer = new Customer { CustomerId = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe" };

            _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest))
                .Returns(customer);

            _repositoryMock.Setup(repo => repo.AddCustomerAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _addHandler.Handle(new AddCustomerCommand(customerRequest), CancellationToken.None);

            // Assert
            var createdAtActionResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult?.ActionName.Should().Be(nameof(CustomerController.Create));
            createdAtActionResult?.RouteValues?["customerId"].Should().Be(customer.CustomerId);
            var response = createdAtActionResult?.Value.Should().BeOfType<ActionDataResponse<CustomerRequest>>().Subject;
            response?.Data.Should().Be(customerRequest);
        }

        [Fact]
        public async Task AddCustomerAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var customerRequest = new CustomerRequest { FirstName = "John", LastName = "Doe" };
            _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _addHandler.Handle(new AddCustomerCommand(customerRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ExistingCustomer_ReturnsNoContent()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRequest = new CustomerRequest { FirstName = "Jane", LastName = "Doe" };
            var existingCustomer = new Customer { CustomerId = customerId.ToString(), FirstName = "John", LastName = "Doe" };
            var newCustomer = new Customer { FirstName = "Jane", LastName = "Doe" };

            _repositoryMock.Setup(repo => repo.GetCustomerByIdAsync(customerId.ToString()))
                .ReturnsAsync(existingCustomer);

            _mapperMock.Setup(mapper => mapper.Map<Customer>(customerRequest))
                .Returns(newCustomer);

            _mapperMock.Setup(mapper => mapper.Map(newCustomer, existingCustomer))
                .Returns(existingCustomer);

            _repositoryMock.Setup(repo => repo.UpdateCustomerAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _updateHandler.Handle(new UpdateCustomerCommand(customerId, customerRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task UpdateCustomerAsync_CustomerNotFound_ReturnsNotFound()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRequest = new CustomerRequest { FirstName = "Jane", LastName = "Doe" };

            _repositoryMock.Setup(repo => repo.GetCustomerByIdAsync(customerId.ToString()))
                .ReturnsAsync((Customer?)null);

            // Act
            var result = await _updateHandler.Handle(new UpdateCustomerCommand(customerId, customerRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task UpdateCustomerAsync_ExceptionThrown_ReturnsErrorObjectResult()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRequest = new CustomerRequest { FirstName = "Jane", LastName = "Doe" };

            _repositoryMock.Setup(repo => repo.GetCustomerByIdAsync(customerId.ToString()))
                .Throws(new Exception("Test exception"));

            // Act
            var result = await _updateHandler.Handle(new UpdateCustomerCommand(customerId, customerRequest), CancellationToken.None);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult?.StatusCode.Should().Be(500);
        }
    }
}
