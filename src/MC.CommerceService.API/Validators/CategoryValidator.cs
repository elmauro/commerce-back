using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of CategoryRequest to ensure all required fields are correctly filled.
    /// </summary>
    public class CategoryValidator : AbstractValidator<CategoryRequest>
    {
        public const string CategoryNameValidator = "Invalid category name provided, please request a category name again.";
        public const string ProductValidator = "Product list cannot be empty.";

        public CategoryValidator()
        {
            // Checks if the 'CategoryName' field of the category is not empty.
            RuleFor(category => category.CategoryName)
                .NotEmpty().WithMessage(CategoryNameValidator);
        }
    }
}
