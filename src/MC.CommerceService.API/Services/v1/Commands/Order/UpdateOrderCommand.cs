using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Orders
{
    /// <summary>
    /// Command to update an existing order in the system.
    /// </summary>
    public class UpdateOrderCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier of the order to be updated.
        /// </summary>
        /// <value>
        /// A GUID representing the unique identifier of the order.
        /// </value>
        public Guid OrderId { get; }

        /// <summary>
        /// The order request containing all necessary data to update the order.
        /// </summary>
        /// <value>
        /// The order request details needed for order update.
        /// </value>
        public OrderRequest Order { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderCommand"/> class.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to update.</param>
        /// <param name="order">The <see cref="OrderRequest"/> containing the details of the order to update.</param>
        public UpdateOrderCommand(Guid orderId, OrderRequest order)
        {
            OrderId = orderId;
            Order = order;
        }
    }
}
