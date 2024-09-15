using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.OrderProducts
{
    /// <summary>
    /// Command to update an existing order-product relationship in the system.
    /// </summary>
    public class UpdateOrderProductCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier for the order.
        /// </summary>
        /// <value>
        /// The GUID representing the unique identifier of the order to update.
        /// </value>
        public Guid OrderId { get; }

        /// <summary>
        /// The unique identifier for the product.
        /// </summary>
        /// <value>
        /// The GUID representing the unique identifier of the product to update.
        /// </value>
        public Guid ProductId { get; }

        /// <summary>
        /// Order-product request containing all necessary data to update the order-product relationship.
        /// </summary>
        /// <value>
        /// The order-product request details needed for the update.
        /// </value>
        public OrderProductRequest OrderProduct { get; }

        /// <summary>
        /// New instance of the <see cref="UpdateOrderProductCommand"/> class.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <param name="orderProduct">The <see cref="OrderProductRequest"/> containing the updated details of the order-product relationship.</param>
        public UpdateOrderProductCommand(Guid orderId, Guid productId, OrderProductRequest orderProduct)
        {
            OrderId = orderId;
            ProductId = productId;
            OrderProduct = orderProduct;
        }
    }
}
