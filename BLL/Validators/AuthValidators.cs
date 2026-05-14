using BLL.DTOs;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Validators
{
    public class PasswordMatchValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as UserDTO;

            if (obj != null && obj.Password.Equals(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Passwords do not match.");
        }
    }

    public class UniqueEmailValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var db = (TenantBridgeContext)validationContext.GetService(typeof(TenantBridgeContext));

            var data = (from u in db.Users where u.Email.Equals(value.ToString()) select u).FirstOrDefault();

            if (data == null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Email already exists.");
        }

    }

}
