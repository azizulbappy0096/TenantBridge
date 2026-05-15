using BLL.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.AuthFilters;

namespace Web.Controllers
{
    [AuthAccess(UserRole.Tenant)]
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
