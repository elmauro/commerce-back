using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of OrderRequest to ensure all required fields are correctly filled.
    /// </summary>
    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        public const string TotalAmountValidator = "Invalid total amount provided, amount must be greater than 0.";
        public const string CustomerIdValidator = "Invalid customer ID provided.";

        public OrderValidator()
        {
            // Checks if the 'TotalAmount' field of the order is greater than 0.
            RuleFor(order => order.TotalAmount)
                .GreaterThan(0).WithMessage(TotalAmountValidator);

            // Checks if the 'CustomerId' field of the order is a valid GUID.
            RuleFor(order => order.CustomerId)
                .NotEmpty().WithMessage(CustomerIdValidator)
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage(CustomerIdValidator);
        }
    }
}
