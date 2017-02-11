using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Wiki.Resources.Models;

namespace Wiki.Models.Biz
{
    public class Article
    {
        [Required]
        [Display(Name = "Titre", ResourceType = typeof(StringsArticle))]
        public string Titre { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Contenu", ResourceType = typeof(StringsArticle))]
        public string Contenu { get; set; }

        [Editable(false)]
        [Display(Name = "Date", ResourceType = typeof(StringsArticle))]
        public DateTime DateModification { get; set; }

        [Editable(false)]
        [Display(Name = "Revision", ResourceType = typeof(StringsArticle))]
        public int Revision { get; set; }

        [Editable(false)]
        [Display(Name = "Contributeur", ResourceType = typeof(StringsArticle))]
        public int IdContributeur { get; set; }

    }
}