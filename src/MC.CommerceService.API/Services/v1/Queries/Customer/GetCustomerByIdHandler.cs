using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Repositories;
using MC.CommerceService.API.Options;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Customer
{
    /// <summary>
    /// Handles the process of retrieving a customer by their unique identifier.
    /// </summary>
    public class GetCustomerByIdHandler : HandlerBase<GetCustomerByIdQuery>
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCustomerByIdHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository used to access customer data.</param>
        /// <param name="logger">Logger used to capture logs and errors during the operation.</param>
        public GetCustomerByIdHandler(ICustomerRepository repository, ILogger<GetCustomerByIdHandler> logger)
            : base(logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Handles the logic for retrieving a customer by their unique identifier.
        /// </summary>
        /// <param name="request">The request containing the customer ID to retrieve.</param>
        /// <param name="cancellationToken">A token used to monitor for cancellation requests.</param>
        /// <returns>An <see cref="IActionResult"/> that contains the customer data or a not-found result.</returns>
        public override async Task<IActionResult> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve the customer using the provided ID
                var customer = await _repository.GetCustomerViewByIdAsync(request.CustomerId.ToString());

                if (customer == null)
                {
                    return new NotFoundResult(); // Return 404 if customer not found
                }

                // Return the customer data in the response
                return new OkObjectResult(new ActionDataResponse<CustomerView>(customer));
            }
            catch (Exception ex)
            {
                // Log any errors that occur during retrieval
                _logger.LogError(ex, "Error occurred while retrieving customer by ID");
                return GetErrorObjectResult();
            }
        }
    }
}
