using BLL.Enums;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Web.AuthFilters;
using Web.Helpers;

namespace Web.Controllers
{
    [AuthAccess(UserRole.Landlord)]
    public class LandlordController : Controller
    {
        PropertyService propertyService;
        CurrentUser currentUser;

        public LandlordController(PropertyService propertyService, CurrentUser currentUser)
        {
            this.propertyService = propertyService;
            this.currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Properties()
        {
            var properties = propertyService.GetByLandlord(this.currentUser.UserId);
            return View(properties);
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
