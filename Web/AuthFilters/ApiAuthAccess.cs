using BLL.Enums;
using DAL.EF.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Web.AuthFilters
{
    public class ApiAuthAccess : Attribute, IAuthorizationFilter
    {
        private readonly UserRole? _role = null;

        public ApiAuthAccess()
        {
        }

        public ApiAuthAccess(UserRole role)
        {
            this._role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var isAuthenticated = context.HttpContext.User.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (this._role.HasValue)
            {
                var hasRole = context.HttpContext.User.IsInRole(((int)this._role.Value).ToString());
                if (!hasRole)
                {
                    context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                    return;
                }
            }
        }
    }
}
