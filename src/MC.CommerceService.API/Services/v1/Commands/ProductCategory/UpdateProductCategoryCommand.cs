using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.ProductCategories
{
    /// <summary>
    /// Command to update an existing product-category relationship.
    /// </summary>
    public class UpdateProductCategoryCommand : IRequest<IActionResult>
    {
        public Guid ProductId { get; }
        public Guid CategoryId { get; }
        public ProductCategoryRequest ProductCategory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductCategoryCommand"/> class.
        /// </summary>
        /// <param name="productId">The product ID in the relationship.</param>
        /// <param name="categoryId">The category ID in the relationship.</param>
        /// <param name="productCategory">The product-category request object with updated details.</param>
        public UpdateProductCategoryCommand(Guid productId, Guid categoryId, ProductCategoryRequest productCategory)
        {
            ProductId = productId;
            CategoryId = categoryId;
            ProductCategory = productCategory;
        }
    }
}
