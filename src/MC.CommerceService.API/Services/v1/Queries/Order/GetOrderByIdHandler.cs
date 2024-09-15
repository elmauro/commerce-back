using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Order
{
    /// <summary>
    /// Handles the retrieval of an order by its unique identifier using the <see cref="GetOrderByIdQuery"/>.
    /// </summary>
    public class GetOrderByIdHandler : HandlerBase<GetOrderByIdQuery>
    {
        private readonly IOrderRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrderByIdHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to interact with the order data.</param>
        /// <param name="logger">The logger used to log information and errors during runtime.</param>
        /// <exception cref="ArgumentNullException">Thrown if the repository is null.</exception>
        public GetOrderByIdHandler(IOrderRepository repository, ILogger<GetOrderByIdHandler> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of retrieving an order by its unique ID.
        /// </summary>
        /// <param name="request">The query containing the order ID.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing the order details if found; otherwise, a 404 response.</returns>
        public override async Task<IActionResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the order by its ID from the repository
                var order = await _repository.GetOrderViewByIdAsync(request.OrderId.ToString());

                if (order == null)
                    return new NotFoundResult();

                // Return the order information wrapped in an ActionDataResponse
                return new OkObjectResult(new ActionDataResponse<OrderView>(order));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order by ID");
                return GetErrorObjectResult();
            }
        }
    }
}
