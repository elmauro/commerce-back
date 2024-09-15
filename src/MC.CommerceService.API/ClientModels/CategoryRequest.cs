using MC.CommerceService.API.Data.Models;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// This class holds information about a category that someone wants to add or change in the system.
    /// </summary>
    public class CategoryRequest
    {
        /// <see cref="Category.CategoryName"/>
        public string CategoryName { get; set; } = string.Empty;
    }
}
