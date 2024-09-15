using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves an order view by its ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>
        /// The result contains the <see cref="OrderView"/> corresponding to the specified order ID, or null if no order is found.
        /// </returns>
        Task<OrderView?> GetOrderViewByIdAsync(string orderId);

        /// <summary>
        /// Retrieves an order entity by its ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the order.</param>
        /// <returns>
        /// The result contains the <see cref="Order"/> entity corresponding to the specified order ID, or null if no order is found.
        /// </returns>
        Task<Order?> GetOrderByIdAsync(string orderId);

        /// <summary>
        /// Adds a new order along with its related products to the database.
        /// </summary>
        /// <param name="order">The order entity to add.</param>
        /// <param name="orderProducts">The list of order-product entities associated with the order.</param>
        Task AddOrderAsync(Order order, List<OrderProduct> orderProducts);

        /// <summary>
        /// Updates an existing order in the database.
        /// </summary>
        /// <param name="order">The order entity to update.</param>
        Task UpdateOrderAsync(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly CommerceDBContext _context;

        public OrderRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<OrderView?> GetOrderViewByIdAsync(string orderId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Select(OrderView.Project())
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        /// <inheritdoc />
        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        /// <inheritdoc />
        public async Task AddOrderAsync(Order order, List<OrderProduct> orderProducts)
        {
            _context.Orders.Add(order);
            _context.OrderProducts.AddRange(orderProducts);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
