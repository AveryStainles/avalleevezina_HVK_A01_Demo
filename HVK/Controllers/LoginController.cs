using HVK.Models;
using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            DataAccessPoint.UpdateLoginUserState(new LoginUserState());
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginUserState loginUserState)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUserState);
            }

            loginUserState.UserType = DataAccessPoint.GetCustomerById(loginUserState.UserId).UserType;
            DataAccessPoint.UpdateLoginUserState(loginUserState);
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet("CreateAccount")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(Customer account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            if (account.UserType == Customer.UserTypes.Customer)
            {
                DataAccessPoint.CreateCustomer(account);
            }
            else
            {
                DataAccessPoint.CreateEmployee(account);
            }

            DataAccessPoint.UpdateLoginUserState(new LoginUserState(account.HVKUserId, account.UserType));
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpGet("GenerateData")]
        public IActionResult GenerateData()
        {
            // For Development Purposes! Test Data 
            if (!DataAccessPoint.GetAllCustomers().Any())
            {
                DataAccessPoint.CreateCustomer(new Customer("Ave", "Coffee"));
                DataAccessPoint.CreateCustomer(new Customer("John", "Doe"));
                DataAccessPoint.CreateCustomer(new Customer("Greg", "Hufferman"));
                DataAccessPoint.CreateEmployee(new Customer("Bossy", "Bossmen"));
                DataAccessPoint.CreateEmployee(new Customer("Rudy", "Judge"));
            }

            if (!DataAccessPoint.GetAllPets().Any())
            {
                DataAccessPoint.CreatePet(new Pet("Rosie", 0));
                DataAccessPoint.CreatePet(new Pet("Bella", 0));
                DataAccessPoint.CreatePet(new Pet("Donna", null));
                DataAccessPoint.CreatePet(new Pet("Jacob", null));
                DataAccessPoint.CreatePet(new Pet("Zach", null));
                DataAccessPoint.CreatePet(new Pet("Audry", null));
                DataAccessPoint.CreatePet(new Pet("Sebastrian", null));

            }

            return RedirectToAction("Index");
        }
    }
}
