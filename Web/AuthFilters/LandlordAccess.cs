using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.AuthFilters
{
    public class LandlordAccess: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRole = context.HttpContext.Session.GetInt32("UserRole");
            if (userRole == null || userRole != (int)BLL.Enums.UserRole.Landlord)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }
        }
    }
}
