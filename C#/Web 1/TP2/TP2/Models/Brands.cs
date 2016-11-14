using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP2.Models
{
    public class Brands
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        [StringLength(100, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(2000, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Description { get; set; }

        [Display(Name = "Site Web")]
        [StringLength(1000, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Website { get; set; }

        [Display(Name = "Adresse")]
        [StringLength(400, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Address { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [StringLength(17, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Phone { get; set; }

    }
}