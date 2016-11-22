using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP2.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Catégorie")]
        [StringLength(80, ErrorMessage = "Le champs {0} ne peut pas être plus long que {2} caractères")]
        public string Name { get; set; }
    }
}