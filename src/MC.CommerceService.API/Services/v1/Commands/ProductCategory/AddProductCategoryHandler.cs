using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.ProductCategories
{
    /// <summary>
    /// Handles the addition of new product-category relationships by processing the <see cref="AddProductCategoryCommand"/>.
    /// </summary>
    public class AddProductCategoryHandler : HandlerBase<AddProductCategoryCommand>
    {
        private readonly IProductCategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCategoryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to interact with the data layer for product-category operations.</param>
        /// <param name="mapper">The mapper to transform data models between request and entity objects.</param>
        /// <param name="logger">The logger to capture runtime logs and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public AddProductCategoryHandler(
            IProductCategoryRepository repository,
            IMapper mapper,
            ILogger<AddProductCategoryHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of adding a new product-category relationship to the system.
        /// </summary>
        /// <param name="request">The command containing the product-category relationship data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add Product-Category operation.</returns>
        public override async Task<IActionResult> Handle(AddProductCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the request DTO to the ProductCategory entity model.
                var newProductCategory = _mapper.Map<ProductCategory>(request.ProductCategory);

                // Add the new product-category relationship to the repository
                await _repository.AddProductCategoryAsync(newProductCategory);

                // Wrap the response in ActionDataResponse and return a CreatedAtActionResult.
                var response = new ActionDataResponse<ProductCategoryRequest>(request.ProductCategory);

                return new CreatedAtActionResult(
                    nameof(ProductCategoryController.Create),
                    null,
                    new { productId = newProductCategory.ProductId, categoryId = newProductCategory.CategoryId },
                    response);
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during processing and return an error response
                _logger.LogError(ex, "Error occurred while adding a new product-category relationship");
                return GetErrorObjectResult();
            }
        }
    }
}
