using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Product
{
    /// <summary>
    /// Handles the retrieval of a paginated list of products.
    /// </summary>
    public class GetPaginatedProductsHandler : HandlerBase<GetPaginatedProductsQuery>
    {
        private readonly IProductRepository _repository;

        public GetPaginatedProductsHandler(
            IProductRepository repository,
            ILogger<GetPaginatedProductsHandler> logger) : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of retrieving a paginated list of products.
        /// </summary>
        public override async Task<IActionResult> Handle(GetPaginatedProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve paginated products from the repository
                var (products, totalCount) = await _repository.GetPaginatedProductsAsync(request.PageNumber, request.PageSize);

                if (products == null || !products.Any())
                    return new NotFoundResult();

                // Map to ProductView
                var productViews = products.Select(ProductView.Project().Compile()).ToList();

                // Construct the paginated response with ActionDataResponse
                var response = new ActionDataResponseList<List<ProductView>>(productViews, totalCount, request.PageNumber, request.PageSize);

                // Return the result
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving paginated products");
                return GetErrorObjectResult();
            }
        }
    }
}
