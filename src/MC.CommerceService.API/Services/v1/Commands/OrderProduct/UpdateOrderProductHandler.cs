using AutoMapper;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.OrderProducts
{
    /// <summary>
    /// Handles the process of updating an existing order-product relationship by processing <see cref="UpdateOrderProductCommand"/>.
    /// </summary>
    public class UpdateOrderProductHandler : HandlerBase<UpdateOrderProductCommand>
    {
        private readonly IOrderProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderProductHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to interact with the order-product data.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for capturing runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required dependency is null.</exception>
        public UpdateOrderProductHandler(
            IOrderProductRepository repository,
            IMapper mapper,
            ILogger<UpdateOrderProductHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the update of an order-product relationship in the database.
        /// </summary>
        /// <param name="request">The command containing the updated order-product data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A result indicating whether the update was successful or not.</returns>
        public override async Task<IActionResult> Handle(UpdateOrderProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the existing order-product relationship from the repository.
                var existingOrderProduct = await _repository.GetOrderProductAsync(request.OrderId.ToString(), request.ProductId.ToString());

                // If the order-product relationship is not found, return a 404 NotFound response.
                if (existingOrderProduct == null)
                    return new NotFoundResult();

                // Map the incoming order-product request onto the existing order-product entity.
                var orderProductToUpdate = _mapper.Map(request.OrderProduct, existingOrderProduct);

                // Update the order-product in the repository.
                await _repository.UpdateOrderProductAsync(orderProductToUpdate);

                // Return a 204 NoContent response indicating that the update was successful.
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // Log any errors and return a generic error response.
                _logger.LogError(ex, "Error occurred while updating order-product relationship with OrderID {OrderId} and ProductID {ProductId}", request.OrderId, request.ProductId);
                return GetErrorObjectResult();
            }
        }
    }
}
