using AutoMapper;
using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Controllers.v1;
using MC.CommerceService.API.Data.Models;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Customers
{
    /// <summary>
    /// Handles the addition of a new customer by processing the <see cref="AddCustomerCommand"/>.
    /// </summary>
    public class AddCustomerHandler : HandlerBase<AddCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddCustomerHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository for interacting with customer data.</param>
        /// <param name="mapper">The mapper to transform data models.</param>
        /// <param name="logger">The logger for logging runtime information and errors.</param>
        public AddCustomerHandler(
            ICustomerRepository repository,
            IMapper mapper,
            ILogger<AddCustomerHandler> logger) : base(mapper, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the process of adding a new customer to the database.
        /// </summary>
        /// <param name="request">The command containing the customer data.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
        /// <returns>
        /// Returns a <see cref="CreatedAtActionResult"/> with the added customer details or an error if the process fails.
        /// </returns>
        public override async Task<IActionResult> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Map the incoming customer DTO to the Customer entity model.
                var newCustomer = _mapper.Map<Customer>(request.Customer);
                newCustomer.CreatedBy = "systemUser";

                // Save the new customer to the database.
                await _repository.AddCustomerAsync(newCustomer);

                // Wrap the customer request in a response object.
                var response = new ActionDataResponse<CustomerRequest>(request.Customer);

                // Return a CreatedAtActionResult with the new customer's ID.
                return new CreatedAtActionResult(nameof(CustomerController.Create), null, new { customerId = newCustomer.CustomerId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new customer");
                return GetErrorObjectResult();
            }
        }
    }
}
