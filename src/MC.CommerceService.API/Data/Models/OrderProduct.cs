using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// Represents the relationship between an order and a product, including quantity information.
    /// </summary>
    public class OrderProduct : IResource
    {
        /// <summary>
        /// The unique identifier of the associated order.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// The order associated with this order-product relationship.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// The unique identifier of the associated product.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The product associated with this order-product relationship.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// The quantity of the product in the order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The identification of the person who first added the product to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the product details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the order-product relationship was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the order-product relationship was updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
