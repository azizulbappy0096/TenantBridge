using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.AuthFilters
{
    public class Logged : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}
