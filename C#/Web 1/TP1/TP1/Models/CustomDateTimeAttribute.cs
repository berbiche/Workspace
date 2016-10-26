using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP1.Models
{
    public class CustomDateTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                return ((DateTime)value).CompareTo(DateTime.Now) == 1 ? ValidationResult.Success : new ValidationResult(ErrorMessage = "La date d'échéance doit être supérieure à la date de création");
            }
            return new ValidationResult(ErrorMessage = "Le champ est requis");
        }
    }
}