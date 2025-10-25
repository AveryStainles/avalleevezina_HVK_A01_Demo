using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HVK.Models
{
    public class Pet
    {
        public enum AnimalGender { M, F }
        public enum Size { S, M, L }

        public Pet()
        {
            PetId = DataAccessPoint.GetPetSequence();
            HVKUserId = new Random().Next(0, DataAccessPoint.GetCustomerSequence());
            Name = "Unknown";
        }
        public Pet(string name, int? ownerId)
        {
            PetId = DataAccessPoint.GetPetSequence();
            HVKUserId = ownerId ?? new Random().Next(0, DataAccessPoint.GetCustomerSequence());
            Name = name;
        }
        public int PetId { get; set; }
        public int HVKUserId { get; set; }
        [Required]
        [MaxLength(25)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [BindRequired]
        [Display(Name = "Gender")]
        public AnimalGender Gender { get; set; }
        [MaxLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Breed")]
        public string? Breed { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "Birth Year")]
        public int? Birthyear { get; set; }
        [Display(Name = "Pet Size")]
        public Size? DogSize { get; set; }
        [Display(Name = "Climber")]
        public bool Climber { get; set; }
        [Display(Name = "Barker")]
        public bool Barker { get; set; }
        [MaxLength(200)]
        [Display(Name = "Special Note")]
        public string? SpecialNotes { get; set; }
        [Display(Name = "Sterilized")]
        public bool Sterilized { get; set; }
    }
}
