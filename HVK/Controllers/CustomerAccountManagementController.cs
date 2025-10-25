using Microsoft.AspNetCore.Mvc;
using HVK.Models;

namespace HVK.Controllers
{
    public class CustomerAccountManagementController : Controller
    {
        public IActionResult Index(string? searchString, string? sortOrder)
        {
            if (DataAccessPoint.GetLoginUserState().UserId == LoginUserState.USER_NOT_LOGGED_IN)
            {
                // Redirect user to login if they are not logged in
                return View("~/Views/Login/Index.cshtml");
            }

            if (DataAccessPoint.GetLoginUserState().UserType == Customer.UserTypes.Customer)
            {
                // Redirect to view profile if logged use is a customer
                return RedirectToAction("ViewCustomer", new { id = DataAccessPoint.GetLoginUserState().UserId });
            }

            ViewData["sortOwner"] = String.IsNullOrEmpty(sortOrder) || sortOrder == "name" ? "namedesc" : "name";
            ViewData["CurrentFilter"] = searchString;
            List<Customer> filteredCustomers = DataAccessPoint.GetAllCustomers();

            if (!String.IsNullOrEmpty(searchString)) { filteredCustomers = DataAccessPoint.GetFilteredCustomerListByString(searchString); }

            return View((!String.IsNullOrEmpty(sortOrder) && sortOrder == "name") ? filteredCustomers.OrderBy(s => s.FirstName) : filteredCustomers.OrderByDescending(s => s.FirstName));
        }

        [HttpGet("CreateCustomer")]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpGet("ViewCustomer")]
        public IActionResult ViewCustomer(int id) => DataAccessPoint.GetLoginUserState().UserId == LoginUserState.USER_NOT_LOGGED_IN
            ? View("~/Views/Login/Index.cshtml")
            : View(DataAccessPoint.GetCustomerById(id));

        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return View(newCustomer);
            }

            DataAccessPoint.CreateCustomer(newCustomer);
            ViewBag.Confirmation = $"Customer \"{newCustomer.FirstName} {newCustomer.LastName}\" was added Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet("UpdateCustomer")]
        public IActionResult UpdateCustomer(int id)
        {
            return View(DataAccessPoint.GetCustomerById(id));
        }

        [HttpPost("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(customerUpdate);
            }

            DataAccessPoint.UpdateCustomerById(customerUpdate.HVKUserId, customerUpdate);
            return RedirectToAction("Index");
        }
    }
}
