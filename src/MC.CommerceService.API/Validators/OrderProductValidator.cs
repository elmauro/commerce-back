using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of OrderProductRequest to ensure all required fields are correctly filled.
    /// </summary>
    public class OrderProductValidator : AbstractValidator<OrderProductRequest>
    {
        public const string ProductIdValidator = "Invalid product ID provided.";
        public const string QuantityValidator = "Invalid quantity provided, quantity must be greater than 0.";

        public OrderProductValidator()
        {
            // Checks if the 'ProductId' field of the order product is a valid GUID.
            RuleFor(orderProduct => orderProduct.ProductId)
                .NotEmpty().WithMessage(ProductIdValidator)
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(ProductIdValidator);

            // Checks if the 'Quantity' field of the order product is greater than 0.
            RuleFor(orderProduct => orderProduct.Quantity)
                .GreaterThan(0).WithMessage(QuantityValidator);
        }
    }
}
