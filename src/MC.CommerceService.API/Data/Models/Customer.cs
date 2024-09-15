using MC.CommerceService.API.Data.ResourceConfiguration;

namespace MC.CommerceService.API.Data.Models
{
    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer : IResource
    {
        /// <summary>
        /// Unique identifier for the customer, created automatically.
        /// </summary>
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The first name of the customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The email address of the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the customer.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The identification of the person who first added the customer to the system.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The identification of the last person to make changes to the customer's details.
        /// </summary>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact date and time when the customer was added to the system.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The exact date and time when the customer's details were last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// A collection of orders placed by the customer.
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}
