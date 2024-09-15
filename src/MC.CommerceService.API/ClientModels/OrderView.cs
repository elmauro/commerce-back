using MC.CommerceService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects an <see cref="Order"/> entity to an <see cref="OrderView"/> model.
    /// </summary>
    public class OrderView
    {
        /// <see cref="Order.OrderId"/>
        public string OrderId { get; set; } = string.Empty;

        /// <see cref="Order.TotalAmount"/>
        public decimal TotalAmount { get; set; }

        /// <see cref="Order.CreatedBy"/>
        public string CreatedBy { get; set; } = string.Empty;

        /// <see cref="Order.LastUpdatedBy"/>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <see cref="Order.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <see cref="Order.LastUpdatedAt"/>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <see cref="Order.CustomerId"/>
        public string CustomerId { get; set; } = string.Empty;

        /// <summary>
        /// Provides a way to automatically create an OrderView from an Order.
        /// </summary>
        public static Expression<Func<Order, OrderView>> Project() => order => new OrderView
        {
            OrderId = order.OrderId.ToString(),
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId.ToString(),
            CreatedBy = order.CreatedBy,
            LastUpdatedBy = order.LastUpdatedBy,
            CreatedAt = order.CreatedAt,
            LastUpdatedAt = order.LastUpdatedAt
        };
    }
}
