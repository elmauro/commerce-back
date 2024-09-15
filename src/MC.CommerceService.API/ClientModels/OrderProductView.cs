using MC.CommerceService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects an <see cref="OrderProduct"/> entity to an <see cref="OrderProductView"/> model.
    /// </summary>
    public class OrderProductView
    {
        /// <see cref="OrderProduct.OrderId"/>
        public string OrderId { get; set; } = string.Empty;

        /// <see cref="OrderProduct.ProductId"/>
        public string ProductId { get; set; } = string.Empty;

        /// <see cref="OrderProduct.Quantity"/>
        public int Quantity { get; set; }

        /// <summary>
        /// Provides a way to automatically create an OrderProductView from an OrderProduct.
        /// </summary>
        public static Expression<Func<OrderProduct, OrderProductView>> Project() => orderProduct => new OrderProductView
        {
            OrderId = orderProduct.OrderId.ToString(),
            ProductId = orderProduct.ProductId.ToString(),
            Quantity = orderProduct.Quantity
        };
    }
}
