using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO.CustomValidation
{
    public class MaximumYearValidationAttribute : ValidationAttribute
    {
        public int MaximumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "Year should be Less than {0}"; // adding default error message
        public MaximumYearValidationAttribute()
        {

        }

        public MaximumYearValidationAttribute(int maximumYear)
        {
            MaximumYear = maximumYear; // adding minimum year as a parameter
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year >= MaximumYear)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MaximumYear));
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
