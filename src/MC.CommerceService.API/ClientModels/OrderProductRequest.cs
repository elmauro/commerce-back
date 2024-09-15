using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a product in an order.
    /// </summary>
    public class OrderProductRequest : ProductQuantityRequest
    {
        /// <see cref="OrderProduct.OrderId"/>
        public Guid OrderId { get; set; }
    }

    public class ProductQuantityRequest
    {
        /// <see cref="OrderProduct.ProductId"/>
        public Guid ProductId { get; set; }

        /// <see cref="OrderProduct.Quantity"/>
        public int Quantity { get; set; }
    }
}
