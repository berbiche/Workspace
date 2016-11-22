using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TP2.Models
{
    public enum Genders
    {
        [Display(Name = "Garçon")]
        M,
        [Display(Name = "Fille")]
        F,
        [Display(Name = "Unisexe")]
        O
    }

    public class Toys
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom du jouet")]
        [StringLength(200, ErrorMessage = "Le nom de jouet ne peut pas dépasser {0} caractères")]
        public string Name { get; set; }

        [Display(Name = "Description du jouet")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, ErrorMessage = "Le nom de jouet ne peut pas dépasser {0} caractères")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Display(Name = "Prix")]
        [Range(0.0, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Date d'ajout")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Sexe")]
        public Genders Gender { get; set; }

        [Display(Name = "Compagnie")]
        public Brands Brand { get; set; }

        //[Display(Name = "Catégorie d'âge")]
        //public AgeCategories Age { get; set; }

        //[Display(Name = "Catégorie de jouet")]
        //public Categories Category { get; set; }


    }
}