using AutoMapper;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Products
{
    /// <summary>
    /// Handles the command to update a product
    /// </summary>
    public class UpdateProductHandler : HandlerBase<UpdateProductCommand>
    {
        protected readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductHandler"/> class.
        /// </summary>
        /// <param name="repository">The product repository.</param>
        /// <param name="mapper">Automapper to map entity and model data.</param>
        /// <param name="logger">Logger for capturing runtime logs.</param>
        public UpdateProductHandler(
            IProductRepository repository,
            IMapper mapper,
            ILogger<UpdateProductHandler> logger) : base(mapper, logger)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the request to update a product in the database.
        /// </summary>
        /// <param name="request">The command containing the product update data.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
        /// <returns></returns>
        public override async Task<IActionResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Fetch the existing product by its ID asynchronously from the repository.
                var existingProduct = await _repository.GetProductByIdAsync(request.ProductId.ToString());

                // Map the incoming product DTO to a new Product entity model.
                var newProduct = _mapper.Map<Product>(request.Product);

                if (existingProduct == null)
                    return new NotFoundResult();

                // Map the updated values from the newly created Product entity to the existing product entity.
                var productToUpdate = _mapper.Map(newProduct, existingProduct);

                // Update the 'LastUpdatedBy' and 'LastUpdatedAt' properties to reflect the current system user and the current UTC time.
                productToUpdate.LastUpdatedBy = systemUser;
                productToUpdate.LastUpdatedAt = DateTime.UtcNow;

                await _repository.UpdateProductAsync(productToUpdate);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product with ID {ProductId}", request.ProductId);
                return GetErrorObjectResult();
            }
        }
    }

}
