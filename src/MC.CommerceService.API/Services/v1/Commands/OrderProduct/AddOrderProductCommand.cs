using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.OrderProducts
{
    /// <summary>
    /// Command to add a new order-product relationship in the system.
    /// </summary>
    public class AddOrderProductCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// Order-product request containing all necessary data to create a new order-product relationship.
        /// </summary>
        /// <value>
        /// The order-product request details needed for the relationship creation.
        /// </value>
        public OrderProductRequest OrderProduct { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddOrderProductCommand"/> class.
        /// </summary>
        /// <param name="orderProduct">
        /// The <see cref="OrderProductRequest"/> containing the details of the order-product relationship to add.
        /// </param>
        public AddOrderProductCommand(OrderProductRequest orderProduct)
        {
            OrderProduct = orderProduct;
        }
    }
}
