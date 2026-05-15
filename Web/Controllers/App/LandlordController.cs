using Microsoft.AspNetCore.Mvc;
using Web.AuthFilters;

namespace Web.Controllers
{
    [Logged]
    [LandlordAccess]
    public class LandlordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Properties()
        {
            return View();
        }

        public IActionResult RentTracker()
        {
            return View();
        }

        public IActionResult DepositLedger()
        {
            return View();
        }

        public IActionResult LeaseAlerts()
        {
            return View();
        }

    }
}
