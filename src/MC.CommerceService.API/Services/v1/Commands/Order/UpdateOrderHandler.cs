using AutoMapper;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Orders
{
    /// <summary>
    /// Handles the process of updating an existing order by processing <see cref="UpdateOrderCommand"/>.
    /// </summary>
    public class UpdateOrderHandler : HandlerBase<UpdateOrderCommand>
    {
        private readonly IOrderRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to interact with the order data.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for logging runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repository is null.</exception>
        public UpdateOrderHandler(
            IOrderRepository repository,
            IMapper mapper,
            ILogger<UpdateOrderHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of updating an existing order.
        /// </summary>
        /// <param name="request">The command containing the updated order data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>A result indicating the success or failure of the update operation.</returns>
        public override async Task<IActionResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the existing order by its ID
                var existingOrder = await _repository.GetOrderByIdAsync(request.OrderId.ToString());

                // Map the updated order details from the request
                var newOrder = _mapper.Map<Order>(request.Order);

                if (existingOrder == null)
                    return new NotFoundResult();

                // Apply the new order details to the existing order and update metadata
                var orderToUpdate = _mapper.Map(newOrder, existingOrder);
                orderToUpdate.LastUpdatedBy = systemUser;  // Assume system user or get from context
                orderToUpdate.LastUpdatedAt = DateTime.UtcNow;

                // Update the order in the repository
                await _repository.UpdateOrderAsync(orderToUpdate);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating order with ID {OrderId}", request.OrderId);
                return GetErrorObjectResult();
            }
        }
    }
}
