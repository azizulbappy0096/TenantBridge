using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs.Auth
{
    public class RegistrationDTO : UserDTO
    {
        public string? PropertyCode { get; set; }
    }
}
