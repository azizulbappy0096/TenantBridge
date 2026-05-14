using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
