using BLL.DTOs;
using BLL.DTOs.Auth;
using BLL.Enums;
using BLL.Services;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

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
                Password =
                {
                    Id = user.Id
                }
            };

            return View(model);
        }

        [HttpPost]
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
            var user = this.authService.Get(obj.Id);
            var model = new SettingsViewModel
            {
                User = user,
                Profile = obj,
                Password =
                {
                    Id = user.Id
                }
            };
            return View("Settings", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordDTO obj)
        {
            if (ModelState.IsValid)
            {
                var success = this.authService.ChangePassword(obj.Id, obj.OldPassword, obj.NewPassword);
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

            var user = this.authService.Get(obj.Id);
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
