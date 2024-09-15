using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.ProductCategories
{
    /// <summary>
    /// Command to add a new product-category relationship in the system.
    /// </summary>
    public class AddProductCategoryCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The product-category request containing the necessary data to create a new product-category relationship.
        /// </summary>
        /// <value>
        /// The product-category request details needed for creating the relationship between a product and a category.
        /// </value>
        public ProductCategoryRequest ProductCategory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductCategoryCommand"/> class.
        /// </summary>
        /// <param name="productCategory">
        /// The <see cref="ProductCategoryRequest"/> containing the details of the product-category relationship to add.
        /// </param>
        public AddProductCategoryCommand(ProductCategoryRequest productCategory)
        {
            ProductCategory = productCategory;
        }
    }
}
