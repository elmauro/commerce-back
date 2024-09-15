using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Retrieves a customer view model by its ID.
        /// </summary>
        /// <param name="customerId">The unique identifier for the customer to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="CustomerView"/>
        /// corresponding to the specified customer ID, or null if no customer is found.
        /// </returns>
        Task<CustomerView?> GetCustomerViewByIdAsync(string customerId);

        /// <summary>
        /// Retrieves a customer entity by its ID.
        /// </summary>
        /// <param name="customerId">The unique identifier for the customer to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="Customer"/> entity corresponding to the specified customer ID, or null if no customer is found.
        /// </returns>
        Task<Customer?> GetCustomerByIdAsync(string customerId);

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">The customer entity to add.</param>
        Task AddCustomerAsync(Customer customer);

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="customer">The customer entity to update.</param>
        Task UpdateCustomerAsync(Customer customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CommerceDBContext _context;

        public CustomerRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<CustomerView?> GetCustomerViewByIdAsync(string customerId)
        {
            return await _context.Customers
                .AsNoTracking()
                .Select(CustomerView.Project())
                .FirstOrDefaultAsync(p => p.CustomerId == customerId);
        }

        /// <inheritdoc />
        public async Task<Customer?> GetCustomerByIdAsync(string customerId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        /// <inheritdoc />
        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
