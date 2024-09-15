using MC.CommerceService.API.ClientModels;
using MC.CommerceService.API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MC.CommerceService.API.Data.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retrieves a category view model by its ID.
        /// </summary>
        /// <param name="categoryId">The unique identifier for the category to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="CategoryView"/>
        /// corresponding to the specified category ID, or null if no category is found.
        /// </returns>
        Task<CategoryView?> GetCategoryViewByIdAsync(string categoryId);

        /// <summary>
        /// Retrieves a category entity by its ID.
        /// </summary>
        /// <param name="categoryId">The unique identifier for the category to retrieve.</param>
        /// <returns>
        /// The result contains the <see cref="Category"/> entity corresponding to the specified category ID, or null if no category is found.
        /// </returns>
        Task<Category?> GetCategoryByIdAsync(string categoryId);

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="category">The category entity to add.</param>
        /// <returns>
        /// The result contains the <see cref="Category"/> entity that was added to the database.
        /// </returns>
        Task AddCategoryAsync(Category category);

        /// <summary>
        /// Updates an existing category in the database.
        /// </summary>
        /// <param name="category">The category entity to update.</param>
        Task UpdateCategoryAsync(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly CommerceDBContext _context;

        public CategoryRepository(CommerceDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<CategoryView?> GetCategoryViewByIdAsync(string categoryId)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(CategoryView.Project())
                .FirstOrDefaultAsync(p => p.CategoryId == categoryId);
        }

        /// <inheritdoc />
        public async Task<Category?> GetCategoryByIdAsync(string categoryId)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
        }

        /// <inheritdoc />
        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
