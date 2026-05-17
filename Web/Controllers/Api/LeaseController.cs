using Microsoft.AspNetCore.Mvc;
using Web.AuthFilters;
using BLL.Enums;
using BLL.Services;
using Web.Helpers;
using BLL.DTOs.FormDTOs;

namespace Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiAuthAccess(UserRole.Landlord)]
    public class LeaseController : Controller
    {
        LeaseService leaseService;
        CurrentUser currentUser;

        public LeaseController(LeaseService leaseService, CurrentUser currentUser)
        {
            this.leaseService = leaseService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public ActionResult Index()
        {

            if(currentUser.Role == (int)UserRole.Landlord)
            {
                var data = this.leaseService.GetByLandlordId(currentUser.UserId);
                return Ok(data);
            }
            else if (currentUser.Role == (int)UserRole.Tenant)
            {
                var data = this.leaseService.GetByTenantId(currentUser.UserId);
                return Ok(data);
            }

            return BadRequest();
        }

        [HttpPost]
        public ActionResult Create([FromForm]LeaseCreateDTO lease)
        {
            lease.LandlordId = this.currentUser.UserId;
            var success = this.leaseService.Create(lease);
            if(success) return Ok(new {success});
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromForm]LeaseCreateDTO lease)
        {
            lease.LandlordId = this.currentUser.UserId;
            var success = this.leaseService.Update(id, lease);
            if (success) return Ok(new { success });
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var success = this.leaseService.Delete(id);
            if (success) return Ok(new { success });
            return BadRequest();
        }


    }
}
