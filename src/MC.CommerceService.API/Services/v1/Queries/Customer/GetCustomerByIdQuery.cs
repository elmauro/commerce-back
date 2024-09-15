using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Customer
{
    /// <summary>
    /// Represents a query to retrieve a customer by their unique identifier.
    /// </summary>
    public class GetCustomerByIdQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> representing the unique identifier of the customer to retrieve.
        /// </value>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCustomerByIdQuery"/> class.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        public GetCustomerByIdQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
