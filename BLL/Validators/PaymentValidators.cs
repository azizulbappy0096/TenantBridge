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

            if (obj != null && obj.Type.Equals("Rent"))
            {
                if (!string.IsNullOrEmpty(value?.ToString()))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Invalid rent month.");

            }

            return ValidationResult.Success;


        }
    }

    public class StatusValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as PaymentCreateDTO;

            if (obj != null && obj.Type.Equals("Rent"))
            {
                if (!string.IsNullOrEmpty(value?.ToString()))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Status is required for rent payments.");

            }

            return ValidationResult.Success;


        }
    }

    public class PaidDateValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as PaymentCreateDTO;

            if (obj != null)
            {
                if (obj.Type.Equals("Deposit") || (obj.Status != null && obj.Status.Equals("Paid")))
                {
                    if (!string.IsNullOrEmpty(value?.ToString()))
                    {
                        return ValidationResult.Success;
                    }
                    return new ValidationResult("Paid date is required");
                }

            }

            return ValidationResult.Success;
        }
    }
}
