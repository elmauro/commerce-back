using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Categories
{
    /// <summary>
    /// Handles the addition of new categories by processing <see cref="AddCategoryCommand"/>.
    /// </summary>
    public class AddCategoryHandler : HandlerBase<AddCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCategoryHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository to interact with the data layer.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for logging runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public AddCategoryHandler(
            ICategoryRepository repository,
            IMapper mapper,
            ILogger<AddCategoryHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the addition of a category to the database.
        /// </summary>
        /// <param name="request">The command containing the category data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result of the Add category operation.</returns>
        public override async Task<IActionResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the incoming category DTO to the Category entity model.
                var newCategory = _mapper.Map<Category>(request.Category);

                // Set the 'CreatedBy' and 'LastUpdatedBy' properties to the current system user.
                newCategory.CreatedBy = systemUser;
                newCategory.LastUpdatedBy = systemUser;

                await _repository.AddCategoryAsync(newCategory);

                var response = new ActionDataResponse<CategoryRequest>(request.Category);

                return new CreatedAtActionResult(nameof(CategoryController.Create), null, new { categoryId = newCategory.CategoryId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new category");
                return GetErrorObjectResult();
            }
        }
    }
}
