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

        public int LandlordId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Rent amount must be greater than 0")]
        public double? RentAmount { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Deposit amount must be greater than 0")]
        public double? DepositAmount { get; set; }

        [Required]
        public DateOnly? StartDate { get; set; }

        [Required]
        public DateOnly? EndDate { get; set; }

    }
}
