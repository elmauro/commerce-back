using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.ProductCategory
{
    /// <summary>
    /// Handles the retrieval of a product-category relationship by its associated product and category IDs.
    /// Processes the <see cref="GetProductCategoryByIdQuery"/> request using the repository and returns a suitable response.
    /// </summary>
    public class GetProductCategoryByIdHandler : HandlerBase<GetProductCategoryByIdQuery>
    {
        private readonly IProductCategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductCategoryByIdHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to access product-category data.</param>
        /// <param name="logger">Logger to capture runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when the repository is null.</exception>
        public GetProductCategoryByIdHandler(IProductCategoryRepository repository, ILogger<GetProductCategoryByIdHandler> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the retrieval of a product-category relationship.
        /// </summary>
        /// <param name="request">The query containing the product and category IDs.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> containing the product-category view, or a not found response if the relationship does not exist.</returns>
        public override async Task<IActionResult> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Attempt to retrieve the product-category relationship by product and category IDs
                var productCategory = await _repository.GetProductCategoryViewByIdAsync(request.ProductId.ToString(), request.CategoryId.ToString());

                if (productCategory == null)
                    return new NotFoundResult();

                // Return the product-category view in a standard action response
                return new OkObjectResult(new ActionDataResponse<ProductCategoryView>(productCategory));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving product-category by product and category ID");
                return GetErrorObjectResult();
            }
        }
    }
}
