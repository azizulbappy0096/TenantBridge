using BLL.Enums;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Web.AuthFilters
{
    public class AuthAccess : Attribute, IAuthorizationFilter
    {
        private readonly UserRole? _role = null;
        private readonly string? _path = null;

        public AuthAccess()
        {
        }

        public AuthAccess(UserRole role)
        {
            this._role = role;
        }

        public AuthAccess(string path)
        {
            this._path = path;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;

            if (!string.IsNullOrEmpty(this._path) && (this._path.ToLower() == "login" || this._path.ToLower() == "register"))
            {
                var CurrentUserRole = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.Role) ?? "0");

                if (CurrentUserRole == (int)UserRole.Tenant)
                {
                    context.Result = new RedirectToActionResult("Index", "Tenant", null);
                    
                }

                if (CurrentUserRole == (int)UserRole.Landlord)
                {
                    context.Result = new RedirectToActionResult("Index", "Landlord", null);
                }

                return;
            }

            

            if(!isAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (this._role.HasValue)
            {
                var hasRole = context.HttpContext.User.IsInRole(((int)this._role.Value).ToString());
                if (!hasRole)
                {
                    context.Result = new RedirectToActionResult("Auth", "Login", null);
                    return;
                }
            }
        }
    }
}
