using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Customers
{
    /// <summary>
    /// Command to update an existing customer in the system.
    /// </summary>
    public class UpdateCustomerCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier of the customer to be updated.
        /// </summary>
        /// <value>
        /// The GUID representing the unique identifier of the customer.
        /// </value>
        public Guid CustomerId { get; }

        /// <summary>
        /// The customer request object containing the updated customer data.
        /// </summary>
        /// <value>
        /// The <see cref="CustomerRequest"/> containing the updated customer details.
        /// </value>
        public CustomerRequest Customer { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomerCommand"/> class.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer to update.</param>
        /// <param name="customer">The updated customer data encapsulated in a <see cref="CustomerRequest"/> object.</param>
        public UpdateCustomerCommand(Guid customerId, CustomerRequest customer)
        {
            CustomerId = customerId;
            Customer = customer;
        }
    }
}
