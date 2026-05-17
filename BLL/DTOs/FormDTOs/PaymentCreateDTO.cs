using BLL.Validators;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs.FormDTOs
{
    public class PaymentCreateDTO
    {
        public int Id { get; set; }

        [Required]
        public string PropertyLeaseId { get; set; } = null!;

        [Required]
        public double? Amount { get; set; }

        [StatusValidator]
        public string? Status { get; set; }

        [Required]
        public string Type { get; set; } = null!;

        [PaidDateValidator]
        public DateTime? PaidDate { get; set; }

        [RentMonthValidator]
        public string? RentMonth { get; set; } 

    }
}
