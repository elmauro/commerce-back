using AutoMapper;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.ProductCategories
{
    /// <summary>
    /// Handles the update of product-category relationships by processing the <see cref="UpdateProductCategoryCommand"/>.
    /// </summary>
    public class UpdateProductCategoryHandler : HandlerBase<UpdateProductCategoryCommand>
    {
        private readonly IProductCategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCategoryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to interact with the data layer for product-category operations.</param>
        /// <param name="mapper">The mapper to transform data models between request and entity objects.</param>
        /// <param name="logger">The logger to capture runtime logs and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public UpdateProductCategoryHandler(
            IProductCategoryRepository repository,
            IMapper mapper,
            ILogger<UpdateProductCategoryHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of updating an existing product-category relationship in the system.
        /// </summary>
        /// <param name="request">The command containing the product-category relationship data to be updated.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Update Product-Category operation.</returns>
        public override async Task<IActionResult> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing ProductCategory relationship from the repository
                var existingProductCategory = await _repository.GetProductCategoryAsync(request.ProductId.ToString(), request.CategoryId.ToString());

                if (existingProductCategory == null)
                    return new NotFoundResult();

                // Map the updated data onto the existing ProductCategory
                var productCategoryToUpdate = _mapper.Map(request.ProductCategory, existingProductCategory);

                // Update the ProductCategory in the repository
                await _repository.UpdateProductCategoryAsync(productCategoryToUpdate);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during processing and return an appropriate error response
                _logger.LogError(ex, "Error occurred while updating product-category relationship with ProductID {ProductId} and CategoryID {CategoryId}", request.ProductId, request.CategoryId);
                return GetErrorObjectResult();
            }
        }
    }
}
