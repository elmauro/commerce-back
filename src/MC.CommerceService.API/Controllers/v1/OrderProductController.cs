using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.CommerceService.API.Services.v1.Queries.OrderProduct;
using MC.CommerceService.API.Services.v1.Commands.OrderProducts;

namespace MC.CommerceService.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class OrderProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the order-product information by order and product ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product in the order.</param>
        /// <response code="200">Returns the order-product information.</response>
        /// <response code="404">The order-product was not found.</response>
        /// <returns>The order-product information if found; otherwise, a 404 status code.</returns>
        [HttpGet("{orderId:guid}/{productId:guid}")]
        [ProducesResponseType(typeof(IActionDataResponse<OrderProductView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid orderId, Guid productId)
        {
            return await _mediator.Send(new GetOrderProductByIdQuery(orderId, productId));
        }

        /// <summary>
        /// Adds a new order-product relationship.
        /// </summary>
        /// <remarks>
        /// Please review the 'orderId' and 'productId' generated in the response headers for further queries or updates.
        /// </remarks>
        /// <param name="orderProduct">The order-product relationship details to add.</param>
        /// <response code="201">The order-product relationship was successfully created.</response>
        /// <response code="400">The order-product information was not valid.</response>
        /// <returns>The created order-product relationship information.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<OrderProductRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrderProductRequest orderProduct)
        {
            return await _mediator.Send(new AddOrderProductCommand(orderProduct));
        }

        /// <summary>
        /// Updates an existing order-product relationship.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product in the order.</param>
        /// <param name="orderProduct">The updated order-product relationship details.</param>
        /// <response code="204">The order-product relationship was successfully updated.</response>
        /// <response code="400">The order-product information was not valid.</response>
        /// <response code="404">The order-product relationship was not found.</response>
        /// <returns>No content if the update is successful; otherwise, an error status code.</returns>
        [HttpPut("{orderId:guid}/{productId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid orderId, [FromRoute] Guid productId, [FromBody] OrderProductRequest orderProduct)
        {
            return await _mediator.Send(new UpdateOrderProductCommand(orderId, productId, orderProduct));
        }
    }
}
