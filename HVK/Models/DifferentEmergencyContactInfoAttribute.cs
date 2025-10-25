using System.ComponentModel.DataAnnotations;

namespace HVK.Models
{
    public class DifferentEmergencyContactInfoAttribute : ValidationAttribute
    {
        public string GetErrorMessage() => $"Emergency Contact Phone Number must be different then the phone number field";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) { return new ValidationResult(GetErrorMessage()); }

            Customer newCustomer = (Customer)value;
            return (newCustomer.Phone != null && newCustomer.Phone == newCustomer.EmergencyContactPhone)
                ? new ValidationResult(GetErrorMessage())
                : ValidationResult.Success;
        }
    }
}
