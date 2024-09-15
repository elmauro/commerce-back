using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Categories
{

    /// <summary>
    /// Command to update an existing category in the system.
    /// </summary>
    public class UpdateCategoryCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// The unique identifier of the category to update.
        /// </summary>
        /// <value>
        /// A <see cref="Guid"/> representing the unique ID of the category.
        /// </value>
        public Guid CategoryId { get; }

        // <summary>
        /// The request containing updated category information.
        /// </summary>
        /// <value>
        /// The <see cref="CategoryRequest"/> containing the new details of the category.
        /// </value>
        public CategoryRequest Category { get; }

        /// <summary>
        /// New instance of the <see cref="UpdateCategoryCommand"/> class with the specified category ID and request.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to update.</param>
        /// <param name="category">The <see cref="CategoryRequest"/> containing the updated category details.</param>
        public UpdateCategoryCommand(Guid categoryId, CategoryRequest category)
        {
            CategoryId = categoryId;
            Category = category;
        }
    }
}
