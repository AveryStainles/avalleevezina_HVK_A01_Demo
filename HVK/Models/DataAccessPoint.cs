using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace HVK.Models
{
    public class DataAccessPoint
    {
        private static List<Customer> _customer = new List<Customer>();
        private static List<Pet> _pet = new List<Pet>();
        private static List<PetVaccination> _petVaccination = new List<PetVaccination>();
        private static List<Vaccination> _vaccination = new List<Vaccination>();
        private static LoginUserState _loginUserState = new LoginUserState();

        #region Sequences
        private static int customerSequence { get; set; } = 0;
        private static int petSequence { get; set; } = 0;
        private static int petVaccinationSequence { get; set; } = 0;
        private static int vaccinationSequence { get; set; } = 0;
        public static int GetCustomerSequence() => customerSequence;
        public static int GetPetSequence() => petSequence;
        public static int GetPetVaccinationSequence() => petVaccinationSequence;
        public static int GetVaccinationSequence() => vaccinationSequence;
        #endregion

        #region Read
        public static LoginUserState GetLoginUserState() => _loginUserState;
        public static List<Customer> GetAllCustomers() => _customer;
        public static List<Pet> GetFilteredPetListByString(string searchString) =>
            GetAllPets().Where(x => x.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                || GetCustomerById(x.HVKUserId).GetFullName.Contains(searchString ?? "", StringComparison.CurrentCultureIgnoreCase)).ToList();
        public static List<Customer> GetFilteredCustomerListByString(string searchString) => GetAllCustomers().Where(x => x.GetFullName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
        public static List<Pet> GetAllPets() => _pet;
        public static List<PetVaccination> GetAllPetVaccination() => _petVaccination;
        public static List<Vaccination> GetAllVaccination() => _vaccination;
        public static Customer GetCustomerById(int id) => _customer.Find(a => a.HVKUserId == id);
        public static List<Pet>? GetAllCustomerPets(int ownerId) => _pet.FindAll(x => x.HVKUserId == ownerId);
        public static Pet GetPetById(int id) => _pet.Find(a => a.PetId == id);
        public static List<PetVaccination> GetPetVaccinationByPetId(int petId) => _petVaccination.FindAll(a => a.PetId == petId);
        public static PetVaccination GetPetVaccination(int petId, int vaccinationId) => _petVaccination.Find(a => a.PetId == petId && a.VaccinationId == vaccinationId);
        public static Vaccination GetVaccinationByVaccinationId(int vaccinationId) => _vaccination.Find(a => a.VaccinationId == vaccinationId);
        public static Vaccination.VaccinationStatus GetVaccinationStatus(int petId)
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Now);
            foreach (PetVaccination petVaccination in DataAccessPoint.GetPetVaccinationByPetId(petId))
            {
                if (!petVaccination.VaccinationChecked)
                {
                    return Vaccination.VaccinationStatus.Invalid;
                }

                if (petVaccination.ExpiryDate < todayDate)
                {
                    return Vaccination.VaccinationStatus.Expired;
                }
            }

            return Vaccination.VaccinationStatus.Valid;
        }
        #endregion

        #region Create
        public static void CreateCustomer(Customer newCustomer)
        {
            newCustomer.HVKUserId = customerSequence++;
            newCustomer.UserType = Customer.UserTypes.Customer;
            _customer.Add(newCustomer);
        }
        public static void CreateEmployee(Customer newEmployee)
        {
            newEmployee.HVKUserId = customerSequence++;
            newEmployee.UserType = Customer.UserTypes.Employee;
            _customer.Add(newEmployee);
        }
        public static void CreatePet(Pet newPet)
        {
            newPet.PetId = petSequence++;
            // Each pet should have a corresponding Vaccination record. By default it assumes no vaccinations were done.
            CreatePetVaccinationProfile(newPet.PetId);
            _pet.Add(newPet);
        }

        public static void CreatePetVaccinationProfile(int petId)
        {
            foreach (var vaccination in _vaccination)
            {
                _petVaccination.Add(new PetVaccination(petId, vaccination));
            }
        }

        public static void CreateVaccination(Vaccination.VaccinationType vaccinationType) => _vaccination.Add(new Vaccination(vaccinationSequence++, vaccinationType));
        #endregion

        #region Update
        public static void UpdateLoginUserState(LoginUserState loginUserStateUpdate) => _loginUserState = loginUserStateUpdate;
        public static void UpdateCustomerById(int id, Customer customer)
        {
            int index = _customer.FindIndex(a => a.HVKUserId == id);
            if (index != -1)
            {
                _customer[index] = customer;
            }
        }
        public static void UpdatePetById(int id, Pet pet)
        {
            int index = _pet.FindIndex(a => a.PetId == id);
            if (index != -1)
            {
                _pet[index] = pet;
            }
        }
        public static void UpdatePetVaccinationByPetId(PetVaccination updatedVaccinations)
        {
            int index = _petVaccination.FindIndex(a => a.PetId == updatedVaccinations.PetId && a.VaccinationId == updatedVaccinations.VaccinationId);
            if (index != -1)
            {
                _petVaccination[index] = updatedVaccinations;
            }
        }
        #endregion

        #region Delete
        public static void DeletePetById(int id)
        {
            int index = _pet.FindIndex(a => a.PetId == id);

            if (index != -1)
            {
                _pet.RemoveAt(index);
                DeletePetVaccinationByPetId(id);
            }
        }

        public static void DeletePetVaccinationByPetId(int petId)
        {
            int index = _petVaccination.FindIndex(a => a.PetId == petId);

            if (index != -1)
            {
                _petVaccination.RemoveAt(index);
            }
        }
        #endregion Delete
    }
}