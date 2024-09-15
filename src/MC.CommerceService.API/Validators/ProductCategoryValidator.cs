using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of ProductCategoryRequest to ensure all required fields are correctly filled.
    /// </summary>
    public class ProductCategoryValidator : AbstractValidator<ProductCategoryRequest>
    {
        public const string ProductIdValidator = "Invalid product ID provided.";
        public const string CategoryIdValidator = "Invalid category ID provided.";

        public ProductCategoryValidator()
        {
            // Checks if the 'ProductId' field of the product-category is a valid GUID.
            RuleFor(productCategory => productCategory.ProductId)
                .NotEmpty().WithMessage(ProductIdValidator)
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(ProductIdValidator);

            // Checks if the 'CategoryId' field of the product-category is a valid GUID.
            RuleFor(productCategory => productCategory.CategoryId)
                .NotEmpty().WithMessage(CategoryIdValidator)
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(CategoryIdValidator);
        }
    }
}
