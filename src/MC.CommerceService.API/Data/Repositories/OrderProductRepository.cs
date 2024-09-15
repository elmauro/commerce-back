using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface IOrderProductRepository
    {
        /// <summary>
        /// Retrieves an order-product view by order ID and product ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>
        /// The result contains the <see cref="OrderProductView"/> 
        /// corresponding to the specified order and product IDs, or null if not found.
        /// </returns>
        Task<OrderProductView?> GetOrderProductViewByIdAsync(string orderId, string productId);

        /// <summary>
        /// Retrieves an order-product entity by order ID and product ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <param name="productId">The unique identifier of the product.</param>
        /// <returns>
        /// The result contains the <see cref="OrderProduct"/> entity corresponding to the specified order and product IDs, or null if not found.
        /// </returns>
        Task<OrderProduct?> GetOrderProductAsync(string orderId, string productId);

        /// <summary>
        /// Adds a new order-product relationship to the database.
        /// </summary>
        /// <param name="orderProduct">The order-product entity to add.</param>
        Task AddOrderProductAsync(OrderProduct orderProduct);

        /// <summary>
        /// Updates an existing order-product relationship in the database.
        /// </summary>
        /// <param name="orderProduct">The order-product entity to update.</param>
        Task UpdateOrderProductAsync(OrderProduct orderProduct);
    }

    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly CommerceDBContext _context;

        public OrderProductRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<OrderProductView?> GetOrderProductViewByIdAsync(string orderId, string productId)
        {
            return await _context.OrderProducts
                .AsNoTracking()
                .Select(OrderProductView.Project())
                .FirstOrDefaultAsync(p => p.OrderId == orderId && p.ProductId == productId);
        }

        /// <inheritdoc />
        public async Task<OrderProduct?> GetOrderProductAsync(string orderId, string productId)
        {
            return await _context.OrderProducts
                .FirstOrDefaultAsync(op => op.OrderId == orderId && op.ProductId == productId);
        }

        /// <inheritdoc />
        public async Task AddOrderProductAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Add(orderProduct);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateOrderProductAsync(OrderProduct orderProduct)
        {
            _context.OrderProducts.Attach(orderProduct);
            _context.Entry(orderProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
