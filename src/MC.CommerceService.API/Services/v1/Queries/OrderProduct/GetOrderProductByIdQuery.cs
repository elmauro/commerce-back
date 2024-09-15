using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.OrderProduct
{
    /// <summary>
    /// Represents a query to retrieve an order-product by its associated order and product IDs.
    /// This query is processed through the MediatR pipeline.
    /// </summary>
    public class GetOrderProductByIdQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier for the order.
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// The unique identifier for the product in the order.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrderProductByIdQuery"/> class.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product in the order.</param>
        public GetOrderProductByIdQuery(Guid orderId, Guid productId)
        {
            OrderId = orderId;
            ProductId = productId;
        }
    }
}
