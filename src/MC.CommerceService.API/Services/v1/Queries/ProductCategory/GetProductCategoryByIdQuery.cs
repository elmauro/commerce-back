using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.ProductCategory
{
    /// <summary>
    /// Represents a query to retrieve a product-category by its product and category ID.
    /// </summary>
    public class GetProductCategoryByIdQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// The ID of the product in the relationship.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The ID of the category in the relationship.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductCategoryByIdQuery"/> class.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="categoryId">The ID of the category.</param>
        public GetProductCategoryByIdQuery(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}
