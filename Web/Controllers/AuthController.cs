using BLL.DTOs.Auth;
using BLL.Enums;
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
            return View(new RegistrationDTO());
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
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO creds)
        {
            if(ModelState.IsValid)
            {
                var user = this.authService.Login(creds.Email, creds.Password);

                if(user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.Id);
                    HttpContext.Session.SetInt32("UserRole", user.Role);
                    
                    if(user.Role == (int)UserRole.Tenant)
                    {
                        return RedirectToAction("Index", "Tenant");
                    }

                    if(user.Role == (int)UserRole.Landlord)
                    {
                        return RedirectToAction("Index", "Landlord");
                    }

                    return RedirectToAction("Index", "Dashboard");

                }
                
            }

            return View(creds);
        }

        public IActionResult Settings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            var user = this.authService.Get((int)userId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            return View(user);
        }

    }
}
