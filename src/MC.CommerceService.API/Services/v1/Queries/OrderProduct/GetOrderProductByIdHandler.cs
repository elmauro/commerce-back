using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using MC.CommerceService.API.Services.v1.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.OrderProduct
{
    /// <summary>
    /// Handles the retrieval of an order-product relationship by order and product IDs.
    /// </summary>
    public class GetOrderProductByIdHandler : HandlerBase<GetOrderProductByIdQuery>
    {
        private readonly IOrderProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrderProductByIdHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to interact with order-product data.</param>
        /// <param name="logger">Logger for capturing runtime logs and errors.</param>
        public GetOrderProductByIdHandler(IOrderProductRepository repository, ILogger<GetOrderProductByIdHandler> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of retrieving an order-product relationship identified by order and product IDs.
        /// </summary>
        /// <param name="request">The request containing the order and product IDs.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>
        /// The result contains the <see cref="OrderProductView"/> corresponding to the specified order and product IDs.
        /// Returns null if no such order-product relationship is found.
        /// </returns>
        public override async Task<IActionResult> Handle(GetOrderProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Attempt to retrieve the order-product by order ID and product ID.
                var orderProduct = await _repository.GetOrderProductViewByIdAsync(request.OrderId.ToString(), request.ProductId.ToString());

                if (orderProduct == null)
                    return new NotFoundResult();

                // Return the found order-product relationship.
                return new OkObjectResult(new ActionDataResponse<OrderProductView>(orderProduct));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving order-product by order and product ID");
                return GetErrorObjectResult();
            }
        }
    }
}
