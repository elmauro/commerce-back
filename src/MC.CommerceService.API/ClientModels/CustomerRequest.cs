using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a customer that someone wants to add or change in the system.
    /// </summary>
    public class CustomerRequest
    {
        /// <see cref="Customer.FirstName"/>
        public string FirstName { get; set; } = string.Empty;

        /// <see cref="Customer.LastName"/>
        public string LastName { get; set; } = string.Empty;

        /// <see cref="Customer.Email"/>
        public string Email { get; set; } = string.Empty;

        /// <see cref="Customer.Phone"/>
        public string Phone { get; set; } = string.Empty;
    }
}
