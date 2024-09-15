using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using MC.CommerceService.API.Services.v1.Queries.Customer;
using MC.CommerceService.API.Services.v1.Commands.Customers;

namespace MC.CommerceService.API.Controllers.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the customer information by ID.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <response code="200">Returns the customer information.</response> 
        /// <response code="404">The customer information was not found.</response>
        /// <returns>The customer information if found; otherwise, a 404 status code.</returns>
        [HttpGet("{customerId:guid}")]
        [ProducesResponseType(typeof(IActionDataResponse<CustomerView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid customerId)
        {
            return await _mediator.Send(new GetCustomerByIdQuery(customerId));
        }

        /// <summary>
        /// Adds a new customer.
        /// </summary>
        /// <remarks>
        /// The 'customerId' is returned in the response headers' 'Location' field, which can be used for future queries or updates.
        /// </remarks>
        /// <param name="customer">The customer information to add.</param>
        /// <response code="201">The customer was successfully created.</response> 
        /// <response code="400">The customer information was not valid.</response>
        /// <returns>The created customer information.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(IActionDataResponse<CustomerRequest>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest customer)
        {
            return await _mediator.Send(new AddCustomerCommand(customer));
        }

        /// <summary>
        /// Updates an existing customer.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to update.</param>
        /// <param name="customer">The updated customer information.</param>
        /// <response code="204">The customer was successfully updated.</response>
        /// <response code="400">The customer information was not valid.</response>
        /// <response code="404">The customer information was not found.</response>
        /// <returns>No content if the update is successful; otherwise, an error status code.</returns>
        [HttpPut("{customerId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid customerId, [FromBody] CustomerRequest customer)
        {
            return await _mediator.Send(new UpdateCustomerCommand(customerId, customer));
        }
    }
}
