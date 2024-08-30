using System.ComponentModel.DataAnnotations;

namespace ECommerceAssignment.CustomValidators
{
    public class MinimumDateValidator : ValidationAttribute
    {
        public string DefaultErrorMessage { get; set; } = "Order date should be greater than or equal to {0}";
        public DateTime MinimumDate { get; set; }

        public MinimumDateValidator(string minimumDateString)
        {
            MinimumDate = Convert.ToDateTime(minimumDateString);
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) 
            {
                DateTime orderDate = Convert.ToDateTime(value);

                if (orderDate < MinimumDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumDate.ToString("yyyy-MM-dd")), new string[] { nameof(validationContext.MemberName) });
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}

