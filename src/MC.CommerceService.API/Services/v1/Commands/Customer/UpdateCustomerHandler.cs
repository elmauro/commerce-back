using AutoMapper;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Customers
{
    /// <summary>
    /// Handles the update of customer details by processing <see cref="UpdateCustomerCommand"/>.
    /// </summary>
    public class UpdateCustomerHandler : HandlerBase<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCustomerHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing customer data.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">Logger for logging runtime information and errors.</param>
        /// <exception cref="ArgumentNullException">Thrown when an injected dependency is null.</exception>
        public UpdateCustomerHandler(
            ICustomerRepository repository,
            IMapper mapper,
            ILogger<UpdateCustomerHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of updating a customer in the database.
        /// </summary>
        /// <param name="request">The command containing the customer update data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>
        /// The result of the customer update operation.
        /// </returns>
        public override async Task<IActionResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the existing customer by ID.
                var existingCustomer = await _repository.GetCustomerByIdAsync(request.CustomerId.ToString());

                // Map the incoming customer DTO to the Customer entity model.
                var newCustomer = _mapper.Map<Customer>(request.Customer);

                // If the customer doesn't exist, return a 404 NotFound result.
                if (existingCustomer == null)
                    return new NotFoundResult();

                // Map the new customer details onto the existing customer entity.
                var customerToUpdate = _mapper.Map(newCustomer, existingCustomer);
                customerToUpdate.LastUpdatedBy = "systemUser";
                customerToUpdate.LastUpdatedAt = DateTime.UtcNow;

                // Save the updated customer to the repository.
                await _repository.UpdateCustomerAsync(customerToUpdate);

                // Return a 204 NoContent result, indicating the update was successful.
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer with ID {CustomerId}", request.CustomerId);
                return GetErrorObjectResult();
            }
        }
    }
}
