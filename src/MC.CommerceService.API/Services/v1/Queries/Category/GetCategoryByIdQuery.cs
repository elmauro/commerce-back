using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Queries.Category
{
    /// <summary>
    /// Represents a query to retrieve a category by its unique identifier.
    /// </summary>
    public class GetCategoryByIdQuery : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier of the category to retrieve.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCategoryByIdQuery"/> class with the specified category identifier.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to retrieve.</param>
        public GetCategoryByIdQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
