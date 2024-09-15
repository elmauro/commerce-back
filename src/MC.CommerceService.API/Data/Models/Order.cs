using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// Represents an order in the system, linking products and customers.
    /// </summary>
    public class Order : IResource
    {
        /// <summary>
        /// Unique identifier for the order, created automatically.
        /// </summary>
        public string OrderId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The date when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// The unique identifier of the customer who placed the order.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The customer who placed the order.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// The identification of the person who first added the order to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the order details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the order was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the order details were changed.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// A collection of products associated with this order.
        /// </summary>
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
