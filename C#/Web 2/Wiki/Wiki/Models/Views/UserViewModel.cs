using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Wiki.Resources.Models;

namespace Wiki.Models.Views
{
    internal static class Constants
    {
        internal const int MinimumPasswordLength = 8;
        internal const int MaximumPasswordLength = 32;
    }
    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = "Courriel", ResourceType = typeof(StringsUserInscription))]
        [DataType(DataType.EmailAddress)]
        public string Courriel { get; set; }
        
        [Required]
        [Display(Name = "Mdp", ResourceType = typeof(StringsUserInscription))]
        [DataType(DataType.Password)]
        [StringLength(Constants.MaximumPasswordLength, ErrorMessageResourceType = typeof(StringsUserInscription), ErrorMessageResourceName = "ErreurMdp", MinimumLength = Constants.MinimumPasswordLength)]
        public string Mdp { get; set; }

        [Display(Name = "Cookie", ResourceType = typeof(StringsUserInscription))]
        public bool Cookie { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }

    public class UserChangeMdpViewModel
    {
        [Key]
        public string Courriel { get; set; }

        [Required]
        [Display(Name = "AncienMdp", ResourceType = typeof(StringsUserChangeMdp))]
        [DataType(DataType.Password)]
        public string AncienMdp { get; set; }

        [Required]
        [Display(Name = "NouveauMdp", ResourceType = typeof(StringsUserChangeMdp))]
        [DataType(DataType.Password)]
        [StringLength(Constants.MaximumPasswordLength, ErrorMessageResourceName = "ErreurMdp", ErrorMessageResourceType = typeof(StringsUserChangeMdp), MinimumLength = Constants.MinimumPasswordLength)]
        public string NouveauMdp { get; set; }
    }

    public class UserProfilViewModel
    {
        [Key]
        public string Courriel { get; set; }

        [Required]
        [Display(Name = "Prenom", ResourceType = typeof(StringsUserInscription))]
        [StringLength(50, ErrorMessageResourceName = "ErreurStringTropLong", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "NomFamille", ResourceType = typeof(StringsUserInscription))]
        [StringLength(50, ErrorMessageResourceName = "ErreurStringTropLong", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string NomFamille { get; set; }

        [Display(Name = "Langue", ResourceType = typeof(StringsUserInscription))]
        public string Langue { get; set; }
    }
    
    public class UserInscriptionViewModel
    {
        [Required]
        [Display(Name = "Courriel", ResourceType = typeof(StringsUserInscription))]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessageResourceName = "ErreurStringTropLong", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string Courriel { get; set; }

        [Required]
        [Display(Name = "Prenom", ResourceType = typeof(StringsUserInscription))]
        [StringLength(50, ErrorMessageResourceName = "ErreurStringTropLong", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string Prenom { get; set; }

        [Required]
        [Display(Name = "NomFamille", ResourceType = typeof(StringsUserInscription))]
        [StringLength(50, ErrorMessageResourceName = "ErreurStringTropLong", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string NomFamille { get; set; }

        [Required]
        [Display(Name = "Mdp", ResourceType = typeof(StringsUserInscription))]
        [DataType(DataType.Password)]
        [StringLength(Constants.MaximumPasswordLength, ErrorMessageResourceName = "ErreurMdp", ErrorMessageResourceType = typeof(StringsUserInscription), MinimumLength = Constants.MinimumPasswordLength)]
        public string Mdp { get; set; }

        [Required]
        [Display(Name = "MdpRepete", ResourceType = typeof(StringsUserInscription))]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Mdp", ErrorMessageResourceName = "ErreurMdpRepete", ErrorMessageResourceType = typeof(StringsUserInscription))]
        public string MdpRepete { get; set; }

        [Display(Name = "Cookie", ResourceType = typeof(StringsUserInscription))]
        public bool Cookie { get; set; }

        [Display(Name = "Langue", ResourceType = typeof(StringsUserInscription))]
        public string Langue { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}