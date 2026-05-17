using BLL.DTOs;
using BLL.DTOs.Auth;
using BLL.Enums;
using BLL.Services;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Web.AuthFilters;
using Web.Helpers;
using Web.Models;

namespace Web.Controllers
{
    
    public class AuthController : Controller
    {
        AuthService authService;
        CurrentUser currentUser;

        public AuthController(AuthService authService, CurrentUser currentUser)
        {
            this.authService = authService;
            this.currentUser = currentUser;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.CurrentUser = currentUser;
            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AuthAccess("Register")]
        public IActionResult Register()
        {
            return View(new RegistrationDTO());
        }

        [HttpPost]
        [AuthAccess("Register")]
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
        [AuthAccess("Login")]
        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        [AuthAccess("Login")]
        public async Task<IActionResult> Login(LoginDTO creds)
        {
            if(ModelState.IsValid)
            {
                var user = this.authService.Login(creds.Email, creds.Password);

                if(user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };
                    
                    var identity = new ClaimsIdentity(claims, "AuthCookie");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("AuthCookie", principal);
                    
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

                TempData["Error"] = "Invalid email or password.";
            }

            return View(creds);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthCookie");
            return RedirectToAction("Login");
        }

        [AuthAccess]
        public IActionResult Settings()
        {
            var user = this.authService.Get(this.currentUser.UserId);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var model = new SettingsViewModel
            {
                User = user,
                Profile =
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                }
            };

            return View(model);
        }

        [HttpPost]
        [AuthAccess]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateProfileDTO obj)
        {
            if (ModelState.IsValid)
            {
                var success = this.authService.Update(obj);
                if(success)
                {
                    TempData["Success"] = "Profile updated successfully!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    TempData["Error"] = "Failed to update profile.";
                }
            }
            var user = this.authService.Get(this.currentUser.UserId);
            var model = new SettingsViewModel
            {
                User = user,
                Profile = obj,
            };
            return View("Settings", model);
        }

        [HttpPost]
        [AuthAccess]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordDTO obj)
        {
            if (ModelState.IsValid)
            {
                var success = this.authService.ChangePassword(this.currentUser.UserId, obj.OldPassword, obj.NewPassword);
                if (success)
                {
                    TempData["Success"] = "Password changed successfully!";
                    return RedirectToAction("Settings");
                }
                else
                {
                    TempData["Error"] = "Failed to change password. Please check your old password.";
                }
            }

            var user = this.authService.Get(this.currentUser.UserId);
            var model = new SettingsViewModel
            {
                User = user,
                Profile =
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                },
                Password = obj
            };

            return View("Settings", model);
        }

    }
}
