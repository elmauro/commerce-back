using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Category
{
    /// <summary>
    /// Handles the retrieval of category details by processing the <see cref="GetCategoryByIdQuery"/>.
    /// </summary>
    public class GetCategoryByIdHandler : HandlerBase<GetCategoryByIdQuery>
    {
        private readonly ICategoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to access category data from the data store.</param>
        /// <param name="logger">Logger to capture runtime information and handle errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public GetCategoryByIdHandler(ICategoryRepository repository, ILogger<GetCategoryByIdHandler> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of retrieving a category identified by its unique identifier.
        /// </summary>
        /// <param name="request">The query containing the category identifier.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>The result contains the <see cref="CategoryView"/> corresponding to the specified category ID. Returns a 404 result if not found.</returns>
        public override async Task<IActionResult> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve category details by the category ID asynchronously
                var category = await _repository.GetCategoryViewByIdAsync(request.CategoryId.ToString());

                if (category == null)
                    return new NotFoundResult();

                // Return the category data in an ActionDataResponse object
                return new OkObjectResult(new ActionDataResponse<CategoryView>(category));
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during processing
                _logger.LogError(ex, "Error occurred while retrieving category by ID");
                return GetErrorObjectResult();
            }
        }
    }
}
