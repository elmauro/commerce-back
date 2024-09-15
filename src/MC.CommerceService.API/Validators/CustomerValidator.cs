using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of CustomerRequest to ensure all required fields are correctly filled.
    /// </summary>
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public const string FirstNameValidator = "Invalid first name provided, please request a first name again.";
        public const string LastNameValidator = "Invalid last name provided, please request a last name again.";
        public const string EmailValidator = "Invalid email provided, please provide a valid email.";
        public const string PhoneValidator = "Invalid phone number provided, please provide a valid phone number.";

        public CustomerValidator()
        {
            // Checks if the 'FirstName' field of the customer is not empty.
            RuleFor(customer => customer.FirstName)
                .NotEmpty().WithMessage(FirstNameValidator);

            // Checks if the 'LastName' field of the customer is not empty.
            RuleFor(customer => customer.LastName)
                .NotEmpty().WithMessage(LastNameValidator);

            // Checks if the 'Email' field of the customer is a valid email.
            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage(EmailValidator)
                .EmailAddress().WithMessage(EmailValidator);

            // Checks if the 'Phone' field of the customer is not empty.
            RuleFor(customer => customer.Phone)
                .NotEmpty().WithMessage(PhoneValidator);
        }
    }
}
