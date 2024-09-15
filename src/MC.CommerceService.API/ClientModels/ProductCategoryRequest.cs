using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about the association between a product and a category.
    /// </summary>
    public class ProductCategoryRequest
    {
        /// <see cref="ProductCategory.ProductId"/>
        public Guid ProductId { get; set; }

        /// <see cref="ProductCategory.CategoryId"/>
        public Guid CategoryId { get; set; }
    }
}
