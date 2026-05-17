using BLL.DTOs;
using BLL.DTOs.FormDTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Validators
{
    public class RentMonthValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as PaymentCreateDTO;

            if (obj != null && obj.Type.Equals("Rent") && !string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid rent month.");
        }
    }
}
