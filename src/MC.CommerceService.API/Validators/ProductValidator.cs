﻿using FluentValidation;
using MC.CommerceService.API.ClientModels;

namespace MC.CommerceService.API.Validators
{
    /// <summary>
    /// Validates the data of ProductRequest to ensure all required fields are correctly filled.
    /// This helps prevent errors by checking the data before processing it further.
    /// </summary>
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public const string ProductTitleValidator = "Invalid product title provided, please request a product name again.";
        public const string ProductDescriptionValidator = "Invalid product id provided, please request a product id again.";
        public const string ProductCodeValidator = "Invalid product code provided, status must be either 0 or 1.";
        public const string ProductPriceValidator = "Invalid product price provided, price must be greater than 0.";
        public const string ProductStockValidator = "Invalid product stock provided, stock must be 0 or more.";

        public ProductValidator()
        {
            // Checks if the 'Totle' field of the product is not empty.
            RuleFor(product => product.Title)
                .NotEmpty().WithMessage(ProductTitleValidator);

            // Checks if the 'Code' field of the product is either 0 or 1.
            RuleFor(product => product.Code)
                .NotEmpty().WithMessage(ProductCodeValidator);

            // Checks if the 'Description' field of the product is not empty.
            RuleFor(product => product.Description)
                .NotEmpty().WithMessage(ProductDescriptionValidator);

            // Checks if the 'Price' field of the product is greater than 0.
            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage(ProductPriceValidator);

            // Checks if the 'Stock' field of the product is 0 or more.
            RuleFor(product => product.Stock)
                .GreaterThanOrEqualTo(0).WithMessage(ProductStockValidator);
        }
    }
}
