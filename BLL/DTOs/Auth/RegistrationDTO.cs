using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs.Auth
{
    public class RegistrationDTO : UserDTO
    {

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;

        public string? PropertyCode { get; set; }
    }
}
