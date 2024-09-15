using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Orders
{
    /// <summary>
    /// Command to add a new order in the system.
    /// </summary>
    public class AddOrderCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The order request containing all necessary data to create a new order.
        /// </summary>
        /// <value>
        /// The <see cref="OrderRequest"/> containing the details of the order to add.
        /// </value>
        public OrderRequest Order { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrderCommand"/> class.
        /// </summary>
        /// <param name="order">The <see cref="OrderRequest"/> containing the details of the order to add.</param>
        public AddOrderCommand(OrderRequest order)
        {
            Order = order;
        }
    }
}
