using System;

namespace HVK.Models
{
    public class PetVaccination
    {
        public int PetId { get; set; }
        public int VaccinationId { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public bool VaccinationChecked { get; set; } = false;
        public PetVaccination()
        {
            PetId = -1;
            VaccinationId = -1;
            ExpiryDate = new DateOnly();
            VaccinationChecked = false;
        }
        public PetVaccination(int petId, Vaccination vaccination, bool vaccinationChecked = false)
        {
            PetId = petId;
            VaccinationId = vaccination.VaccinationId;
            ExpiryDate = new DateOnly();
            VaccinationChecked = vaccinationChecked;
        }
    }
}
