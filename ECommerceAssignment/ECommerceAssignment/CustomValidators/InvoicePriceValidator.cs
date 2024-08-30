using System.ComponentModel.DataAnnotations;
using System.Reflection;
using ECommerceAssignment.Models;

namespace ECommerceAssignment.CustomValidators
{
    public class InvoicePriceValidator :ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";

        public InvoicePriceValidator()
        {
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {


            if(value != null)
            {
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));


                if (otherProperty != null) 
                {
                    List<Product> products = (List<Product>)otherProperty.GetValue(validationContext.ObjectInstance)!;

                    double priceSum = 0;

                    if (products == null)
                    {
                        return null;
                    }

                    foreach (Product product in products) 
                    {
                        priceSum += (product.Price * product.Quantity);
                    }

                    if (priceSum > 0) 
                    {
                        if(priceSum != Convert.ToDouble(value))
                        {
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, priceSum), new string[] { nameof(validationContext.MemberName) });
                        }
                        else
                        {
                            return ValidationResult.Success;
                        }
                    }
                    return null;
                }

                return null;
            }

            return null;

        }
    }
}
