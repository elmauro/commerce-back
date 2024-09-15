using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Product
{
    /// <summary>
    /// Represents a query to retrieve a paginated list of products.
    /// </summary>
    public class GetPaginatedProductsQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// The page number to retrieve.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of products per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaginatedProductsQuery"/> class.
        /// </summary>
        public GetPaginatedProductsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
