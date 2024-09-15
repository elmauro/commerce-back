using MC.CommerceService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects a <see cref="Product"/> entity to a <see cref="ProductView"/> model.
    /// </summary>
    /// <returns>
    /// Helps to convert a Product's data into a ProductView format.
    /// </returns>
    public class ProductView
    {
        /// <see cref="Product.Title"/>
        public string Title { get; set; } = string.Empty;

        /// <see cref="Product.Code"/>
        public string Code { get; set; }

        /// <see cref="Product.Description"/>
        public string Description { get; set; } = string.Empty;

        /// <see cref="Product.Price"/>
        public decimal Price { get; set; }

        /// <see cref="Product.Stock"/>
        public int Stock { get; set; }

        /// <see cref="Product.ProductId"/>
        public string ProductId { get; set; } = string.Empty;

        /// <see cref="Product.CreatedBy"/>
        public string CreatedBy { get; set; } = string.Empty;

        /// <see cref="Product.LastUpdatedBy"/>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <see cref="Product.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <see cref="Product.LastUpdatedAt"/>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Provides a way to automatically create a ProductView from a Product.
        /// </summary>
        public static Expression<Func<Product, ProductView>> Project() => product => new ProductView
        {
            ProductId = product.ProductId,
            Title = product.Title,
            Code = product.Code,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CreatedBy = product.CreatedBy,
            LastUpdatedBy = product.LastUpdatedBy,
            CreatedAt = product.CreatedAt,
            LastUpdatedAt = product.LastUpdatedAt
        };
    }
}
