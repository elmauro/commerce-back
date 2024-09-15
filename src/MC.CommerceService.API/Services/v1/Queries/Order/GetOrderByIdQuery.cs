using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Order
{
    /// <summary>
    /// Represents a query to retrieve an order by its unique identifier.
    /// </summary>
    public class GetOrderByIdQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> representing the order's unique identifier.
        /// </value>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetOrderByIdQuery"/> class.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order to retrieve.</param>
        public GetOrderByIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
