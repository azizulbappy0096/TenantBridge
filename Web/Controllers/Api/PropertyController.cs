using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        PropertyService propertyService;

        public PropertyController(PropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = propertyService.Get();

            return Ok(data);
        }

    }
}
