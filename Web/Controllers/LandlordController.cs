using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LandlordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
