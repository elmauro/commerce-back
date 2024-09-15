using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a product view model by its ID.
        /// </summary>
        /// <param name="productId">The unique identifier for the product to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="ProductView"/>
        /// corresponding to the specified product ID, or null if no product is found.
        /// </returns>
        Task<ProductView?> GetProductViewByIdAsync(string productId);

        /// <summary>
        /// Retrieves a product entity by its ID.
        /// </summary>
        /// <param name="productId">The unique identifier for the product to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="Product"/>
        /// entity corresponding to the specified product ID, or null if no product is found.
        /// </returns>
        Task<Product?> GetProductByIdAsync(string productId);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="product">The product entity to add.</param>
        /// <returns>
        /// The result contains the <see cref="Product"/> entity that was added to the database.
        /// </returns>
        Task AddProductAsync(Product product);

        /// <summary>
        /// Updates an existing product in the database.
        /// </summary>
        /// <param name="product">The product entity to update.</param>
        Task UpdateProductAsync(Product product);

        /// <summary>
        /// Retrieves a paginated list of products and the total product count.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of products per page.</param>
        /// <returns>
        /// A tuple containing a list of products for the requested page and the total count of products.
        /// </returns>
        Task<(List<Product> Products, int TotalCount)> GetPaginatedProductsAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves a list of products by their IDs.
        /// </summary>
        /// <param name="productIds">A collection of product IDs to retrieve.</param>
        /// <returns>
        /// A list of <see cref="Product"/> entities corresponding to the provided product IDs.
        /// </returns>
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<string> productIds);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly CommerceDBContext _context;

        public ProductRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ProductView?> GetProductViewByIdAsync(string productId)
        {
            return await _context.Products
                .AsNoTracking()
                .Select(ProductView.Project())
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        /// <inheritdoc />
        public async Task<Product?> GetProductByIdAsync(string productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        /// <inheritdoc />
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<(List<Product> Products, int TotalCount)> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            var productsQuery = _context.Products.AsQueryable();

            var totalCount = await productsQuery.CountAsync(); // Get the total number of products

            var products = await productsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); // Get the products for the current page

            return (products, totalCount);
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<string> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.ProductId.ToString()))
                .ToListAsync();
        }
    }
}
