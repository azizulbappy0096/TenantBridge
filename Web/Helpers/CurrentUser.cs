using System.Security.Claims;

namespace Web.Helpers
{
    public class CurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => int.Parse(this._httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
        public int Role => int.Parse(this._httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role) ?? "0");
        public string Email => this._httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email) ?? "";
        public string FullName => this._httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? "";
    }
}
