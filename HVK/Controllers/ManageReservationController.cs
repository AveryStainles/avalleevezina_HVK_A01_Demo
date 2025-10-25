using Microsoft.AspNetCore.Mvc;

namespace HVK.Controllers
{
    public class ManageReservationController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/PageUnderConstruction/Index.cshtml");
        }
    }
}
