using HVK.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HVK.Controllers
{
    public class PetManagementController : Controller
    {
        public IActionResult Index()
        {
            if (DataAccessPoint.GetLoginUserState().UserId == LoginUserState.USER_NOT_LOGGED_IN)
            {
                // Redirect user to login if they are not logged in
                return View("~/Views/Login/Index.cshtml");
            }

            if (DataAccessPoint.GetLoginUserState().UserType == Customer.UserTypes.Customer)
            {
                return RedirectToAction("Index", new { ownerId = DataAccessPoint.GetLoginUserState().UserId });
            }

            // Display all Pets
            return View(DataAccessPoint.GetAllPets());
        }


        [HttpGet("PetManagement/{ownerId}")]
        public IActionResult Index(int ownerId)
        {
            if (DataAccessPoint.GetLoginUserState().UserId == LoginUserState.USER_NOT_LOGGED_IN)
            {
                // Redirect user to login if they are not logged in
                return View("~/Views/Login/Index.cshtml");
            }

            ViewData["ownerId"] = ownerId;
            return View(DataAccessPoint.GetAllCustomerPets(ownerId));
        }

        [HttpGet("ViewPet")]
        public IActionResult ViewPet(int id) => DataAccessPoint.GetLoginUserState().UserId == LoginUserState.USER_NOT_LOGGED_IN
            ? View("~/Views/Login/Index.cshtml")
            : View(DataAccessPoint.GetPetById(id));

        [HttpGet("SortPet")]
        public IActionResult SortPet(string sortOrder, string searchString)
        {
            //Track Sorting Order and Search Filter
            ViewData["sortName"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "name" ? "namedesc" : "name";
            ViewData["sortOwner"] = sortOrder == "ownerdesc" ? "owner" : "ownerdesc";
            ViewData["CurrentFilter"] = searchString;

            if (!DataAccessPoint.GetAllPets().Any()) { return RedirectToAction("Index"); }

            List<Pet> filteredPets = DataAccessPoint.GetAllPets();
            if (!String.IsNullOrEmpty(searchString)) { filteredPets = DataAccessPoint.GetFilteredPetListByString(searchString); }

            switch (sortOrder)
            {
                case "namedesc":
                    return View("~/Views/PetManagement/Index.cshtml", filteredPets.OrderByDescending(s => s.Name));
                case "owner":
                    return View("~/Views/PetManagement/Index.cshtml", filteredPets.OrderBy(s => DataAccessPoint.GetCustomerById(s.HVKUserId).GetFullName.ToLower()));
                case "ownerdesc":
                    return View("~/Views/PetManagement/Index.cshtml", filteredPets.OrderByDescending(s => DataAccessPoint.GetCustomerById(s.HVKUserId).GetFullName.ToLower()));
                case "name":
                default:
                    return View("~/Views/PetManagement/Index.cshtml", filteredPets.OrderBy(s => s.Name));
            }

        }

        [HttpGet("CreatePet")]
        public IActionResult CreatePet(int ownerId)
        {
            ViewData["ownerId"] = ownerId;
            return View();
        }

        [HttpPost("CreatePet")]
        public IActionResult CreatePet(Pet newPet)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ownerId"] = newPet.HVKUserId;
                return View(newPet);
            }
            DataAccessPoint.CreatePet(newPet);
            ViewBag.Confirmation = $"Pet \"{newPet.PetId}:{newPet.HVKUserId} | {newPet.Name}\" was added Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet("UpdatePet")]
        public IActionResult UpdatePet(int id)
        {
            return View(DataAccessPoint.GetPetById(id));
        }

        [HttpPost("UpdatePet")]
        public IActionResult UpdatePet(Pet petUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(petUpdate);
            }

            DataAccessPoint.UpdatePetById(petUpdate.PetId, petUpdate);

            return RedirectToAction("Index", new { ownerId = DataAccessPoint.GetPetById(petUpdate.PetId).HVKUserId });
        }

        [HttpGet("DeletePet")]
        public IActionResult DeletePet(int id)
        {
            return View(DataAccessPoint.GetPetById(id));
        }

        [HttpPost("ConfirmedDeletePet")]
        public IActionResult ConfirmedDeletePet(int petId)
        {
            int customerId = DataAccessPoint.GetPetById(petId).HVKUserId;
            DataAccessPoint.DeletePetById(petId);
            return RedirectToAction("Index", new { ownerId = customerId });
        }
    }
}
