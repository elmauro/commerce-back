using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.CommerceService.API.Services.v1.Queries.ProductCategory;
using MC.CommerceService.API.Services.v1.Commands.ProductCategories;

namespace MC.CommerceService.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the product-category relationship by product and category ID.
        /// </summary>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <param name="categoryId">The unique identifier of the category.</param>
        /// <response code="200">Returns the product-category relationship.</response>
        /// <response code="404">The product-category relationship was not found.</response>
        /// <returns>The product-category relationship if found; otherwise, a 404 status code.</returns>
        [HttpGet("{productId:guid}/{categoryId:guid}")]
        [ProducesResponseType(typeof(IActionDataResponse<ProductCategoryView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid productId, Guid categoryId)
        {
            return await _mediator.Send(new GetProductCategoryByIdQuery(productId, categoryId));
        }

        /// <summary>
        /// Adds a new product-category relationship.
        /// </summary>
        /// <remarks>
        /// Ensure that the product and category exist before creating the relationship.
        /// </remarks>
        /// <param name="productCategory">The product-category relationship details to add.</param>
        /// <response code="201">The product-category relationship was successfully created.</response>
        /// <response code="400">The product-category information was not valid.</response>
        /// <returns>The created product-category relationship information.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<ProductCategoryRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductCategoryRequest productCategory)
        {
            return await _mediator.Send(new AddProductCategoryCommand(productCategory));
        }

        /// <summary>
        /// Updates an existing product-category relationship.
        /// </summary>
        /// <param name="productId">The unique identifier of the product in the relationship.</param>
        /// <param name="categoryId">The unique identifier of the category in the relationship.</param>
        /// <param name="productCategory">The updated product-category details.</param>
        /// <response code="204">The product-category relationship was successfully updated.</response>
        /// <response code="400">The product-category information was not valid.</response>
        /// <response code="404">The product-category relationship was not found.</response>
        /// <returns>No content if the update is successful; otherwise, an error status code.</returns>
        [HttpPut("{productId:guid}/{categoryId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid productId, [FromRoute] Guid categoryId, [FromBody] ProductCategoryRequest productCategory)
        {
            return await _mediator.Send(new UpdateProductCategoryCommand(productId, categoryId, productCategory));
        }
    }
}
