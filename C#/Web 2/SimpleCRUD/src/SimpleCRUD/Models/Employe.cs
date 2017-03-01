using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCRUD.Models
{
    public class Employe
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20), StringLength(20)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(20), StringLength(20)]
        public string Prenom { get; set; }

        [MaxLength(20), StringLength(20)]
        public string Fonction { get; set; }

        [DataType(DataType.Currency)]
        [Range(20000, 120000)]
        public decimal Salaire { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEmbauche { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [MaxLength(20), StringLength(20)]
        public string Pays { get; set; }

    }
}
