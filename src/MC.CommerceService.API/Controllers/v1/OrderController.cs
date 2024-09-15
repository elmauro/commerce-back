using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.CommerceService.API.Services.v1.Queries.Order;
using MC.CommerceService.API.Services.v1.Commands.Orders;

namespace MC.CommerceService.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the order information by ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <response code="200">Returns the order information.</response> 
        /// <response code="404">The order was not found.</response>
        /// <returns>The order information if found; otherwise, a 404 status code.</returns>
        [HttpGet("{orderId:guid}")]
        [ProducesResponseType(typeof(IActionDataResponse<OrderView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid orderId)
        {
            return await _mediator.Send(new GetOrderByIdQuery(orderId));
        }

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <remarks>
        /// The 'orderId' returned in the response headers can be used for querying or updating the order information.
        /// </remarks>
        /// <param name="order">The order details to add.</param>
        /// <response code="201">The order was successfully created.</response> 
        /// <response code="400">The order information was not valid.</response>
        /// <returns>The created order information.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<OrderRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrderRequest order)
        {
            return await _mediator.Send(new AddOrderCommand(order));
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to update.</param>
        /// <param name="order">The updated order details.</param>
        /// <response code="204">The order was successfully updated.</response>
        /// <response code="400">The order details were not valid.</response>
        /// <response code="404">The order was not found.</response>
        /// <returns>No content if the update is successful; otherwise, an error status code.</returns>
        [HttpPut("{orderId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid orderId, [FromBody] OrderRequest order)
        {
            return await _mediator.Send(new UpdateOrderCommand(orderId, order));
        }
    }
}
