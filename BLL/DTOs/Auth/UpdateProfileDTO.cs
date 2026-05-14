using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTOs.Auth
{
    public class UpdateProfileDTO
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FullName { get; set; }

        
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
