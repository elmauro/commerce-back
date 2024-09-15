using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.OrderProducts
{
    /// <summary>
    /// Handles the addition of new order-product relationships by processing <see cref="AddOrderProductCommand"/>.
    /// </summary>
    public class AddOrderProductHandler : HandlerBase<AddOrderProductCommand>
    {
        private readonly IOrderProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrderProductHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to interact with the data layer for order-product operations.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for logging runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public AddOrderProductHandler(
            IOrderProductRepository repository,
            IMapper mapper,
            ILogger<AddOrderProductHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of adding a new order-product relationship to the database.
        /// </summary>
        /// <param name="request">The command containing the order-product data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add order-product operation.</returns>
        public override async Task<IActionResult> Handle(AddOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the incoming order-product DTO to the OrderProduct entity model.
                var newOrderProduct = _mapper.Map<OrderProduct>(request.OrderProduct);

                // Save the new order-product to the database.
                await _repository.AddOrderProductAsync(newOrderProduct);

                // Wrap the request data in an ActionDataResponse and return a CreatedAtActionResult.
                var response = new ActionDataResponse<OrderProductRequest>(request.OrderProduct);
                return new CreatedAtActionResult(nameof(OrderProductController.Create), null, new { orderId = newOrderProduct.OrderId, productId = newOrderProduct.ProductId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new order-product relationship");
                return GetErrorObjectResult();
            }
        }
    }
}
