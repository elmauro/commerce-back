using MC.CommerceService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects a <see cref="Customer"/> entity to a <see cref="CustomerView"/> model.
    /// </summary>
    public class CustomerView
    {
        /// <see cref="Customer.CustomerId"/>
        public string CustomerId { get; set; } = string.Empty;

        /// <see cref="Customer.FirstName"/>
        public string FirstName { get; set; } = string.Empty;

        /// <see cref="Customer.LastName"/>
        public string LastName { get; set; } = string.Empty;

        /// <see cref="Customer.Email"/>
        public string Email { get; set; } = string.Empty;

        /// <see cref="Customer.Phone"/>
        public string Phone { get; set; } = string.Empty;

        /// <see cref="Customer.CreatedBy"/>
        public string CreatedBy { get; set; } = string.Empty;

        /// <see cref="Customer.LastUpdatedBy"/>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <see cref="Customer.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <see cref="Customer.LastUpdatedAt"/>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Provides a way to automatically create a CustomerView from a Customer.
        /// </summary>
        public static Expression<Func<Customer, CustomerView>> Project() => customer => new CustomerView
        {
            CustomerId = customer.CustomerId.ToString(),
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
            CreatedBy = customer.CreatedBy,
            LastUpdatedBy = customer.LastUpdatedBy,
            CreatedAt = customer.CreatedAt,
            LastUpdatedAt = customer.LastUpdatedAt
        };
    }
}
