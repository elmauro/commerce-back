using MC.CommerceService.API.Data.Models;
using System.Linq.Expressions;

namespace MC.CommerceService.API.ClientModels
{
    /// <summary>
    /// Projects a <see cref="Category"/> entity to a <see cref="CategoryView"/> model.
    /// </summary>
    public class CategoryView
    {
        /// <see cref="Category.CategoryId"/>
        public string CategoryId { get; set; } = string.Empty;

        /// <see cref="Category.CategoryName"/>
        public string CategoryName { get; set; } = string.Empty;

        /// <see cref="Category.CreatedBy"/>
        public string CreatedBy { get; set; } = string.Empty;

        /// <see cref="Category.LastUpdatedBy"/>
        public string LastUpdatedBy { get; set; } = string.Empty;

        /// <see cref="Category.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <see cref="Category.LastUpdatedAt"/>
        public DateTimeOffset LastUpdatedAt { get; set; }

        /// <summary>
        /// Provides a way to automatically create a CategoryView from a Category.
        /// </summary>
        public static Expression<Func<Category, CategoryView>> Project() => category => new CategoryView
        {
            CategoryId = category.CategoryId.ToString(),
            CategoryName = category.CategoryName,
            CreatedBy = category.CreatedBy,
            LastUpdatedBy = category.LastUpdatedBy,
            CreatedAt = category.CreatedAt,
            LastUpdatedAt = category.LastUpdatedAt
        };
    }
}
