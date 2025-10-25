using HVK.Models;
using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class VaccinationController : Controller
    {
        public IActionResult Index(int petId)
        {
            return View(DataAccessPoint.GetPetVaccinationByPetId(petId));
        }

        [HttpPost]
        public IActionResult Index(IEnumerable<PetVaccination> newPetVaccination)
        {
            if (!ModelState.IsValid)
            {
                return View(newPetVaccination);
            }

            foreach (var item in newPetVaccination)
            {
                DataAccessPoint.UpdatePetVaccinationByPetId(item);
            }

            ViewBag.Confirmation = $"Pet Vaccination updated successfully";
            return View("~/Views/PetManagement/ViewPet.cshtml", DataAccessPoint.GetPetById(newPetVaccination.ToList()[0].PetId));
        }

    }
}
