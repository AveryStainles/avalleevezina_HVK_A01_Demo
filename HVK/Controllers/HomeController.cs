using HVK.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet("Search")]
        public IActionResult Search(string? searchString)
        {
            return ((String.IsNullOrEmpty(searchString))) 
                ? RedirectToAction("Index", "Home") 
                : RedirectToAction("SortPet", "PetManagement", new { sortOrder = "", searchString }); 
        }
    }
}
