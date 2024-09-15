using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface IProductCategoryRepository
    {
        /// <summary>
        /// Retrieves a product-category view by the product ID and category ID.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="categoryId">The unique identifier for the category.</param>
        /// <returns>
        /// The result contains the <see cref="ProductCategoryView"/> 
        /// corresponding to the specified product and category IDs, or null if no relationship is found.
        /// </returns>
        Task<ProductCategoryView?> GetProductCategoryViewByIdAsync(string productId, string categoryId);

        /// <summary>
        /// Retrieves a product-category entity by the product ID and category ID.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="categoryId">The unique identifier for the category.</param>
        /// <returns>
        /// The result contains the <see cref="ProductCategory"/> 
        /// entity corresponding to the specified product and category IDs, or null if no relationship is found.
        /// </returns>
        Task<ProductCategory?> GetProductCategoryAsync(string productId, string categoryId);

        /// <summary>
        /// Adds a new product-category relationship to the database.
        /// </summary>
        /// <param name="productCategory">The product-category entity to add.</param>
        Task AddProductCategoryAsync(ProductCategory productCategory);

        /// <summary>
        /// Updates an existing product-category relationship in the database.
        /// </summary>
        /// <param name="productCategory">The product-category entity to update.</param>
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
    }

    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly CommerceDBContext _context;

        public ProductCategoryRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ProductCategoryView?> GetProductCategoryViewByIdAsync(string productId, string categoryId)
        {
            return await _context.ProductCategory
                .AsNoTracking()
                .Select(ProductCategoryView.Project()) // Projecting to the ProductCategoryView model
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        /// <inheritdoc />
        public async Task<ProductCategory?> GetProductCategoryAsync(string productId, string categoryId)
        {
            return await _context.ProductCategory
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        /// <inheritdoc />
        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategory.Add(productCategory);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategory.Attach(productCategory);
            _context.Entry(productCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
