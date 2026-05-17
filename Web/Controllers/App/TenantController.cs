using BLL.DTOs;
using BLL.Enums;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.AuthFilters;
using Web.Helpers;

namespace Web.Controllers
{
    [AuthAccess(UserRole.Tenant)]
    public class TenantController : Controller
    {

        AuthService authService;
        PropertyService propertyService;
        PaymentService paymentService;
        LeaseService leaseService;
        AnalyticService analyticService;

        CurrentUser currentUser;

        public TenantController(AuthService authService, PropertyService propertyService, PaymentService paymentService, LeaseService leaseService, AnalyticService analyticService, CurrentUser currentUser)
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
            var analyticsData = this.analyticService.GetTenantAnalytics(currentUser.UserId);
            return View(analyticsData);
        }

        public IActionResult RentTracker()
        {
            var data = this.paymentService.GetByTenantId(this.currentUser.UserId, "Rent");

            var analytics = this.analyticService.GetRentAnalyticsForTenant(this.currentUser.UserId);

            ViewBag.Analytics = analytics;

            return View(data);
        }

        public IActionResult DepositLedger()
        {

            var data = this.paymentService.GetByTenantId(this.currentUser.UserId, "Deposit");

            var totalDeposit = data.Sum(p => p.Amount);

            ViewBag.TotalDeposit = totalDeposit;

            return View(data ?? new List<PaymentDTO>());

        }

        public IActionResult LeaseAlerts()
        {
            var data = this.leaseService.GetByTenantId(this.currentUser.UserId);

            var analytics = this.analyticService.GetLeaseAnalyticsForTenant(currentUser.UserId);

            ViewBag.Analytics = analytics;

            return View(data);

        }
    }
}
