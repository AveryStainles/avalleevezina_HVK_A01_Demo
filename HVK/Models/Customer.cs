using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using HVK.Models;
using static HVK.Models.Customer;

namespace HVK.Models
{
    [DifferentEmergencyContactInfo]
    [RequiredPhoneOrLogin]
    public class Customer
    {
        private const string PHONE_REGEX_PATTERN = "^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$";
        public enum Provinces { QC, ON, AL, SA };
        public enum UserTypes { Customer, Employee };

        public Customer(string firstName = "Unknown", string lastName = "Unknown", Provinces province = Provinces.QC, string phone = "1231231234")
        {
            HVKUserId = DataAccessPoint.GetCustomerSequence();
            FirstName = firstName;
            LastName = lastName;
            Province = province;
            Phone = phone;
        }

        public Customer()
        {
            HVKUserId = DataAccessPoint.GetCustomerSequence();
            FirstName = "Unknown";
            LastName = "Unknown";
            Province = Provinces.QC;
            Phone = "1231231234";
        }
        public int HVKUserId { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MaxLength(50)]
        [RegularExpression("^\\S+@\\S+\\.\\S+$")]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [MaxLength(40)]
        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "Province")]
        public Provinces? Province { get; set; }
        [RegularExpression("^[a-zA-Z]\\d[a-zA-Z][ -]?\\d[a-zA-Z]\\d$")]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        [RegularExpression(PHONE_REGEX_PATTERN)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [RegularExpression(PHONE_REGEX_PATTERN)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Cell-Phone Number")]
        public string? CellPhone { get; set; }

        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "Emergency Contact First Name")]
        public string? EmergencyContactFirstName { get; set; }

        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "Emergency Contact Last Name")]
        public string? EmergencyContactLastName { get; set; }

        [RegularExpression(PHONE_REGEX_PATTERN)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Emergency Contact Phone Number")]
        public string? EmergencyContactPhone { get; set; }
        public UserTypes UserType { get; set; }
        public string GetFullName => $"{this.FirstName} {this.LastName}";
    }
}
