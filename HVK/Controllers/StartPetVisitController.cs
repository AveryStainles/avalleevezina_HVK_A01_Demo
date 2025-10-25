using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class StartPetVisitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
