using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs.FormDTOs
{
    public class LeaseCreateDTO
    {
        [Required]
        public int PropertyId { get; set; }

        [Required]
        public int LandlordId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public double RentAmount { get; set; }

        [Required]
        public double DepositAmount { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

    }
}
