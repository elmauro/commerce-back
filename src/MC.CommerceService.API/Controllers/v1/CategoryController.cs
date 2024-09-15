using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.CommerceService.API.Services.v1.Queries.Category;
using MC.CommerceService.API.Services.v1.Commands.Categories;

namespace MC.CommerceService.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the category information by ID.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <response code="200">Returns the category information.</response> 
        /// <response code="404">The category information was not found.</response>
        /// <returns>The category information if found; otherwise, a 404 status code.</returns>
        [HttpGet("{categoryId:guid}")]
        [ProducesResponseType(typeof(IActionDataResponse<CategoryView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid categoryId)
        {
            return await _mediator.Send(new GetCategoryByIdQuery(categoryId));
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <remarks>
        /// The 'categoryId' is returned in the response headers' 'Location' field, which can be used for future queries or updates.
        /// </remarks>
        /// <param name="category">The category information to add.</param>
        /// <response code="201">The category was successfully created.</response> 
        /// <response code="400">The category information was not valid.</response>
        /// <returns>The created category information.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<CategoryRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CategoryRequest category)
        {
            return await _mediator.Send(new AddCategoryCommand(category));
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to update.</param>
        /// <param name="category">The updated category information.</param>
        /// <response code="204">The category was successfully updated.</response>
        /// <response code="400">The category information was not valid.</response>
        /// <response code="404">The category information was not found.</response>
        /// <returns>No content if the update is successful; otherwise, an error status code.</returns>
        [HttpPut("{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] CategoryRequest category)
        {
            return await _mediator.Send(new UpdateCategoryCommand(categoryId, category));
        }
    }
}
