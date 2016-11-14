using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TP2.Models.CustomValidationAttribute;

namespace TP2.Models
{
    public class AgeCategories
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nom de la catégorie d'âge")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Âge de début")]
        [Range(0, 17, ErrorMessage = "L'{0} doit être entre {1} et {2}")]
        public int AgeStart { get; set; }

        [Required]
        [Display(Name = "Âge de fin")]
        [Range(1, 18, ErrorMessage = "L'{0} doit être entre {1} et {2}")]
        [SuperiorToAgeStart]
        public int AgeEnd { get; set; }

    }
}