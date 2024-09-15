namespace MC.CommerceService.API.Options
{
    /// <summary>
    /// This interface sets up a standard way for API responses to show data.
    /// It makes sure all data is shown under a 'data' section, following rules found at a web resource.
    /// </summary>
    /// <typeparam name="T">The type of data returned in the response.</typeparam>
    public interface IActionDataResponse<T>
    {
        /// <summary>
        /// The main data provided by the response.
        /// </summary>
        T Data { get; set; }
    }

    /// <summary>
    /// This class makes sure that all responses from different parts of the API look the same.
    /// </summary>
    /// <typeparam name="T">The type of the data included in the response.</typeparam>
    public class ActionDataResponse<T> : IActionDataResponse<T>
    {
        public T Data { get; set; }

        /// <summary>
        /// Creates a new response that wraps the provided data.
        /// </summary>
        /// <param name="data">The data to be included in the response.</param>
        public ActionDataResponse(T data)
        {
            Data = data;
        }

        // Additional properties and methods could be added here if needed
    }

    public class ActionDataResponseList<T> : ActionDataResponse<T>, IActionDataResponse<T>
    {
        // Pagination metadata
        public int TotalProducts { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        /// <summary>
        /// Creates a new response that wraps the provided data along with pagination info.
        /// </summary>
        /// <param name="data">The data to be included in the response.</param>
        /// <param name="totalProducts">Total number of products.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">The page size.</param>
        public ActionDataResponseList(T data, int totalProducts, int currentPage, int pageSize) : base(data)
        {
            Data = data;
            TotalProducts = totalProducts;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        // Additional properties and methods could be added here if needed
    }

    /// <summary>
    /// This class represents an error response when an API request does not pass validation.
    /// </summary>
    public class ErrorResponse
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? TraceId { get; set; }
    }

    /// <summary>
    /// Provides detailed information about the errors that occurred during the validation of an API request.
    /// </summary>
    public class ValidationErrorResponse : ErrorResponse
    {
        /// <summary>
        /// A list of errors, showing what went wrong during validation.
        /// </summary>
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
