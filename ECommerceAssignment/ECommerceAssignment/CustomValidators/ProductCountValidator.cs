using ECommerceAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAssignment.CustomValidators
{
    public class ProductCountValidator : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Order should have at least one product";

        public ProductCountValidator()
        {
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            List<Product> products = (List<Product>)value;
            if (products != null)
            {
                if (products.Count <= 0)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
        }
    }
}
