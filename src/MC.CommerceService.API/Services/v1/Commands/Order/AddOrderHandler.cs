using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Orders
{
    /// <summary>
    /// Handles the process of adding a new order by processing the <see cref="AddOrderCommand"/>.
    /// </summary>
    public class AddOrderHandler : HandlerBase<AddOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrderHandler"/> class.
        /// </summary>
        /// <param name="orderRepository">The repository to interact with orders in the data layer.</param>
        /// <param name="customerRepository">The repository to interact with customers in the data layer.</param>
        /// <param name="productRepository">The repository to interact with products in the data layer.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for logging runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when a repository or mapper is null.</exception>
        public AddOrderHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IMapper mapper,
            ILogger<AddOrderHandler> logger) : base(mapper, logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// Handles the process of adding a new order and its related products to the database.
        /// </summary>
        /// <param name="request">The command containing the order data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add order operation, with created status if successful.</returns>
        public override async Task<IActionResult> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the customer
                var customer = await _customerRepository.GetCustomerByIdAsync(request.Order.CustomerId.ToString());
                if (customer == null)
                {
                    return new NotFoundObjectResult(new ErrorResponse { Title = "Customer not found", Status = 404 });
                }

                // Validate the products
                var productIds = request.Order.Products.Select(p => p.ProductId.ToString()).ToList();
                var products = await _productRepository.GetProductsByIdsAsync(productIds);
                if (products.Count != request.Order.Products.Count)
                {
                    return new BadRequestObjectResult(new ErrorResponse { Title = "One or more products not found", Status = 400 });
                }

                // Create a new order entity
                var newOrder = _mapper.Map<Order>(request.Order);
                newOrder.CreatedBy = systemUser;  // Assume system user or get from context
                newOrder.LastUpdatedBy = systemUser;

                // Create the OrderProduct entities
                var orderProducts = request.Order.Products.Select(op => new OrderProduct
                {
                    OrderId = newOrder.OrderId,
                    ProductId = op.ProductId.ToString(),
                    Quantity = op.Quantity,
                    CreatedAt = DateTimeOffset.UtcNow,
                    LastUpdatedAt = DateTimeOffset.UtcNow
                }).ToList();

                // Save the order and related order products
                await _orderRepository.AddOrderAsync(newOrder, orderProducts);

                var response = new ActionDataResponse<OrderRequest>(request.Order);
                return new CreatedAtActionResult(nameof(OrderController.Create), null, new { orderId = newOrder.OrderId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order");
                return GetErrorObjectResult();
            }
        }
    }
}
