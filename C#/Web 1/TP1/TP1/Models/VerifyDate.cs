using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP1.Models
{
    public class VerifyDate : ValidationAttribute
    {

        //public DateTime date = DateTime.Now;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if ((DateTime) value >= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("La date d'échéance doit être supérieure à la date de création");
            }
        }
    }
}