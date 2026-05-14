using BLL.DTOs;
using BLL.DTOs.Auth;

namespace Web.Models
{
    public class SettingsViewModel
    {
        public UserDTO User { get; set; } = new();
        public UpdateProfileDTO Profile { get; set; } = new();

        public ChangePasswordDTO Password { get; set; } = new();
    }
}
