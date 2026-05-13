using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserDTO());
        }

        [HttpPost]
        public IActionResult Register(RegistrationDTO user)
        {
            if(ModelState.IsValid)
            {
                this.authService.Register(user);
                TempData["Success"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            return View();
        }


    }
}
