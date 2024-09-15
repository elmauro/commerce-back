using MC.CommerceService.API.ClientModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MC.CommerceService.API.Services.v1.Commands.Categories
{
    /// <summary>
    /// Command to add a new category in the system.
    /// </summary>
    public class AddCategoryCommand : IRequest<IActionResult>
    {
        /// <summary>
        /// Category request containing all necessary data to create a new category.
        /// </summary>
        /// <value>
        /// The category request details needed for category creation.
        /// </value>
        public CategoryRequest Category { get; set; }

        /// <summary>
        /// New instance of the <see cref="AddCategoryCommand"/> class.
        /// </summary>
        /// <param name="category">
        /// The <see cref="CategoryRequest"/> containing the details of the category to add.
        /// </param>
        public AddCategoryCommand(CategoryRequest category)
        {
            Category = category;
        }
    }
}
