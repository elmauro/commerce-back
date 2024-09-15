using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a product that someone wants to add or change in the system.
    /// </summary>
    public class ProductRequest
    {
        /// <see cref="Product.Title"/>
        public string Title { get; set; } = string.Empty;

        /// <see cref="Product.Code"/>
        public string Code{ get; set; } = string.Empty;

        /// <see cref="Product.Description"/>
        public string Description { get; set; } = string.Empty;

        /// <see cref="Product.Price"/>
        public decimal Price { get; set; }

        /// <see cref="Product.Stock"/>
        public int Stock { get; set; }
    }
}
