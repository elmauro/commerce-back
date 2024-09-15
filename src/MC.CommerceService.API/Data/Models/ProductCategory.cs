using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// Represents the relationship between a product and a category.
    /// </summary>
    public class ProductCategory : IResource
    {
        /// <summary>
        /// The unique identifier of the associated product.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// The product associated with this product-category relationship.
        /// </summary>
        public Product Product { get; set; }  // Navigation property

        /// <summary>
        /// The unique identifier of the associated category.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// The category associated with this product-category relationship.
        /// </summary>
        public Category Category { get; set; }  // Navigation property

        /// <summary>
        /// The identification of the person who first created the product-category relationship.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to update the product-category relationship.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the product-category relationship was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The last time the product-category relationship was updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }
    }
}
