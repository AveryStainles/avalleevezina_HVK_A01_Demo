using System.ComponentModel.DataAnnotations;

namespace HVK.Models
{
    public class Vaccination
    {
        public enum VaccinationStatus
        {
            Valid,
            Invalid,
            Expired
        }

        public enum VaccinationType
        {
            Bordetella,
            Distemper,
            Hepatitis,
            Parainfluenza,
            Parovirus,
            Rabies
        }
        public int VaccinationId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public Vaccination()
        {
            VaccinationId = -1;
            Name = string.Empty;
        }

        public Vaccination(int id, VaccinationType name)
        {
            VaccinationId = id;
            Name = name.ToString();
        }

    }
}
