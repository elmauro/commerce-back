using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Product
{
    /// <summary>
    /// Handles the retrieval of product details by product ID.
    /// </summary>
    public class GetProductByIdHandler : HandlerBase<GetProductByIdQuery>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// New instance of the <see cref="GetProductByIdHandler"/>.
        /// </summary>
        /// <param name="repository">The product repository for data access.</param>
        /// <param name="logger">Logger for capturing runtime logs and errors.</param>
        public GetProductByIdHandler(
            IProductRepository repository,
            ILogger<GetProductByIdHandler> logger) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of retrieving a product identified by ID.
        /// </summary>
        /// <param name="request">The ID of the product to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns>The result contains the <see cref="ProductView"/> 
        /// corresponding to the specified product ID. Returns null if no product is found with the provided ID.
        /// </returns>
        public override async Task<IActionResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Attempt to retrieve a product by its ID asynchronously.
                var product = await _repository.GetProductViewByIdAsync(request.ProductId.ToString());

                if (product == null)
                    return new NotFoundResult();


                // Return the updated product object with the discount applied.
                return new OkObjectResult(new ActionDataResponse<ProductView>(product));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product by ID");
                return GetErrorObjectResult();
            }
        }
    }
}
