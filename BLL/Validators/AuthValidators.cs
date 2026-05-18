using BLL.DTOs;
using BLL.DTOs.Auth;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.Validators
{
    public class NewPasswordValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as ChangePasswordDTO;

            if (obj != null && !obj.OldPassword.Equals(value.ToString()))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("New password cannot be the same as the old password.");
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
