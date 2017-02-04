using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Required]
        public string Titre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Contenu { get; set; }

        [Editable(false)]
        [Display(Name = "Date de modification")]
        public DateTime DateModification { get; set; }

        [Editable(false)]
        [Display(Name = "Nombre de révision")]
        public int Revision { get; set; }

        [Editable(false)]
        [Display(Name = "Identifiant du contributeur")]
        public int IdContributeur { get; set; }

    }
}