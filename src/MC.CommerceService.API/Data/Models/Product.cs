using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// This class represents a product in the system. It allows the product to be saved and found in the database.
    /// </summary>
    public class Product : IResource
    {
        /// <summary>
        /// Unique identifier for the product, created automatically.
        /// </summary>
        public string ProductId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name or title of the product.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Product code, like SKU.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Brief description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The number of items in stock.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// The user who first created the product.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The user who last updated the product.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the product was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The exact date and time when the product was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Categories related to this product.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Orders that contain this product.
        /// </summary>
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
