using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class EmployeeHomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
