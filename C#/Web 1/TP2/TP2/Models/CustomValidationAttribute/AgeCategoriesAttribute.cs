using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP2.Models.CustomValidationAttribute
{
    public class SuperiorToAgeStartAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            AgeCategories age = (AgeCategories) validationContext.ObjectInstance;
            if (age.AgeEnd > age.AgeStart)
                return ValidationResult.Success;
            return new ValidationResult("Le champ \"Âge de fin\" doit être supérieur au champ \"Âge de début\"");
        }
    }
}