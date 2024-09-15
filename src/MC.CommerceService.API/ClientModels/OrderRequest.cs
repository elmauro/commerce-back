using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about an order that someone wants to add or change in the system.
    /// </summary>
    public class OrderRequest
    {
        /// <see cref="Order.TotalAmount"/>
        public decimal TotalAmount { get; set; }

        /// <see cref="Order.OrderDate"/>
        public DateTime OrderDate { get; set; }

        /// <see cref="Order.CustomerId"/>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// A list of products and their quantities that are part of this order.
        /// </summary>
        public List<ProductQuantityRequest> Products { get; set; }
    }
}
