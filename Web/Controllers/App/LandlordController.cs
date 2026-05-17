using BLL.Enums;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.AuthFilters;
using Web.Helpers;

namespace Web.Controllers
{
    [AuthAccess(UserRole.Landlord)]
    public class LandlordController : Controller
    {
        AuthService authService;
        PropertyService propertyService;
        PaymentService paymentService;
        LeaseService leaseService;
        AnalyticService analyticService;

        CurrentUser currentUser;

        public LandlordController(AuthService authService, PropertyService propertyService, PaymentService paymentService, LeaseService leaseService, AnalyticService analyticService, CurrentUser currentUser)
        {
            this.authService = authService;
            this.propertyService = propertyService;
            this.paymentService = paymentService;
            this.leaseService = leaseService;
            this.analyticService = analyticService;
            this.currentUser = currentUser;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.CurrentUser = currentUser;
            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            var analyticsData = this.analyticService.GetLandlordAnalytics(currentUser.UserId);
            return View(analyticsData);
        }

        public IActionResult Properties()
        {
            var properties = propertyService.GetByLandlord(this.currentUser.UserId);
            return View(properties);
        }

        public IActionResult RentTracker()
        {
            var data = this.paymentService.GetByLandlordId(this.currentUser.UserId, "Rent");
            var leases = this.leaseService.GetByLandlordId(this.currentUser.UserId);

            var analytics = this.paymentService.GetRentAnalyticsForLandlord(this.currentUser.UserId);

            ViewBag.Leases = leases;
            ViewBag.Analytics = analytics;

            return View(data);
        }

        public IActionResult DepositLedger()
        {

            var data = this.paymentService.GetByLandlordId(this.currentUser.UserId, "Deposit");
            var leases = this.leaseService.GetByLandlordId(this.currentUser.UserId);

            ViewBag.Leases = leases;

            return View(data);

        }

        public IActionResult LeaseAlerts()
        {


            var data = this.leaseService.GetByLandlordId(this.currentUser.UserId);
            var properties = this.propertyService.GetByLandlord(this.currentUser.UserId);
            var tenants = this.authService.GetByRole((int)UserRole.Tenant);

            ViewBag.Properties = properties;
            ViewBag.Tenants = tenants;

            return View(data);

        }

    }
}
