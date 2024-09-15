using AutoMapper;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Categories
{
    /// <summary>
    /// Handles the update of an existing category by processing the <see cref="UpdateCategoryCommand"/>.
    /// </summary>
    public class UpdateCategoryHandler : HandlerBase<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCategoryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository for interacting with the category data.</param>
        /// <param name="mapper">The mapper to map data between models.</param>
        /// <param name="logger">The logger to record runtime logs and errors.</param>
        public UpdateCategoryHandler(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<UpdateCategoryHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of updating an existing category in the database.
        /// </summary>
        /// <param name="request">The command containing the updated category data and category ID.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a <see cref="NoContentResult"/> if the update was successful, or a <see cref="NotFoundResult"/>
        /// if the category was not found.
        /// </returns>
        public override async Task<IActionResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the existing category by its ID.
                var existingCategory = await _repository.GetCategoryByIdAsync(request.CategoryId.ToString());

                // Map the updated category data.
                var newCategory = _mapper.Map<Category>(request.Category);

                // If the category does not exist, return NotFound.
                if (existingCategory == null)
                    return new NotFoundResult();

                // Map the new category data onto the existing category.
                var categoryToUpdate = _mapper.Map(newCategory, existingCategory);
                categoryToUpdate.LastUpdatedBy = systemUser;
                categoryToUpdate.LastUpdatedAt = DateTime.UtcNow;

                // Perform the update operation.
                await _repository.UpdateCategoryAsync(categoryToUpdate);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating category with ID {CategoryId}", request.CategoryId);
                return GetErrorObjectResult();
            }
        }
    }
}
