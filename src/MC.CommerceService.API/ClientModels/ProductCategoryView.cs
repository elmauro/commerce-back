using System;
using System.Linq.Expressions;
using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects a <see cref="ProductCategory"/> entity to a <see cref="ProductCategoryView"/> model.
    /// </summary>
    public class ProductCategoryView
    {
        /// <see cref="ProductCategory.ProductId"/>
        public string ProductId { get; set; } = string.Empty;

        /// <see cref="ProductCategory.CategoryId"/>
        public string CategoryId { get; set; } = string.Empty;

        /// <summary>
        /// Provides a way to automatically create a ProductCategoryView from a ProductCategory.
        /// </summary>
        public static Expression<Func<ProductCategory, ProductCategoryView>> Project() => productCategory => new ProductCategoryView
        {
            ProductId = productCategory.ProductId.ToString(),
            CategoryId = productCategory.CategoryId.ToString()
        };
    }
}
