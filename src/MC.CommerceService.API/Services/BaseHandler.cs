using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services
{
    public abstract class HandlerBase<T> : IRequestHandler<T, IActionResult> where T : IRequest<IActionResult>
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly string _internalServerErrorMessage = "Something went wrong, please try again later.";
        protected const string systemUser = "system";

        /// <summary>
        /// Initializes a new instance of the ProductHandlerBase class.
        /// </summary>
        /// <param name="mapper">Automapper to map entity and model data.</param>
        /// <param name="logger">Logger for capturing runtime logs.</param>
        public HandlerBase(IMapper mapper, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public HandlerBase(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates an ObjectResult for internal server errors.
        /// </summary>
        /// <returns>An ObjectResult configured for internal server errors.</returns>
        protected ObjectResult GetErrorObjectResult()
        {
            return new ObjectResult(_internalServerErrorMessage)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        public abstract Task<IActionResult> Handle(T request, CancellationToken cancellationToken);
    }
}
