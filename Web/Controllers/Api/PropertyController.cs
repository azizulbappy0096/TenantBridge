using BLL.DTOs;
using BLL.Services;
using Web.AuthFilters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BLL.Enums;
using Web.Helpers;

namespace Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiAuthAccess(UserRole.Landlord)]
    public class PropertyController : Controller
    {
        PropertyService propertyService;
        CurrentUser currentUser;

        public PropertyController(PropertyService propertyService, CurrentUser currentUser)
        {
            this.propertyService = propertyService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = propertyService.Get();

            return Ok(data);
        }

        [HttpPost]
        public ActionResult Create(
            [FromForm]
            [Required]
            [StringLength(100, MinimumLength = 2)]
            string address
            )
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var property = new PropertyDTO
            {
                LandlordId = currentUser.UserId,
                Address = address,
                Active = true
            };
            var success = propertyService.Create(property);
            if (success) return Ok(new { success });
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = propertyService.Delete(id);
            if (success) return Ok(new { success });
            return BadRequest();

        }

    }
}
