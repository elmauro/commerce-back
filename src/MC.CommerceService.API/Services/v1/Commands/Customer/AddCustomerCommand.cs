using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Customers
{
    /// <summary>
    /// Command to add a new customer in the system.
    /// </summary>
    public class AddCustomerCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The customer request containing all necessary data to create a new customer.
        /// </summary>
        /// <value>
        /// The customer details needed for customer creation.
        /// </value>
        public CustomerRequest Customer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCustomerCommand"/> class.
        /// </summary>
        /// <param name="customer">
        /// The <see cref="CustomerRequest"/> containing the details of the customer to add.
        /// </param>
        public AddCustomerCommand(CustomerRequest customer)
        {
            Customer = customer;
        }
    }
}