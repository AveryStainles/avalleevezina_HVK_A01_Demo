using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class CustomerHomePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
