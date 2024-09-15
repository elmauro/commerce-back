using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// Represents a category in the system, used to group related products.
    /// </summary>
    public class Category : IResource
    {
        /// <summary>
        /// Unique identifier for the category, created automatically.
        /// </summary>
        public string CategoryId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// The user who first added the category.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The user who last updated the category.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the category was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The exact date and time when the category was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Collection of products that belong to this category, represented by a relationship with <see cref="ProductCategory"/>.
        /// </summary>
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
