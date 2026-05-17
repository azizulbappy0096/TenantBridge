using BLL.DTOs.FormDTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Web.AuthFilters;
using Web.Helpers;

namespace Web.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiAuthAccess(BLL.Enums.UserRole.Landlord)]
    public class PaymentController : Controller
    {
        PaymentService paymentService;
        CurrentUser currentUser;

        public PaymentController(PaymentService paymentService, CurrentUser currentUser)
        {
            this.paymentService = paymentService;
            this.currentUser = currentUser;
        }


        [HttpPost]
        public ActionResult Index([FromForm] PaymentCreateDTO payment)
        {
            var success = this.paymentService.Create(payment);
            if (success) return Ok(new { success });
            return BadRequest();
        }

        [HttpPut("{id}")]
        public ActionResult Index(int id,
            [FromForm]
            [Required]
            string Status)
        {
            var success = this.paymentService.Update(id, Status);
            if (success) return Ok(new { success });
            return BadRequest();
        }
    }
}
