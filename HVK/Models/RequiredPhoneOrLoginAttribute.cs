using System.ComponentModel.DataAnnotations;

namespace HVK.Models
{
    public class RequiredPhoneOrLoginAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => $"Form must contain phone number or login details password and email";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult(GetErrorMessage());
            }

            Customer newCustomer = (Customer)value;

            if (newCustomer.Phone == null && (newCustomer.Email == null || newCustomer.Password == null))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
