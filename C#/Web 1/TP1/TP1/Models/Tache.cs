using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TP1.Models
{
    public class Tache
    {
        [Key, Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "La description de la tâche est requise")]
        [Display(Name = "Description")]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le nom du client est requis!")]
        [Display(Name = "Client")]
        public string Client { get; set; }
        [Required(ErrorMessage = "La priorité de la tâche est nécessaire")]
        [Range(1, 3, ErrorMessage = "La valeur doit être comprise entre 1 et 3"), Display(Name = "Priorité")]
        [DefaultValue(1)]
        public int Priority { get; set; }
        [Required(ErrorMessage = "La date d'écheance est requise")]
        [DataType(DataType.DateTime)]
        [Timestamp, Display(Name = "Échéance")]
        [CustomValidation(typeof(Tache), "DateValide")]
        [DefaultValue(typeof(DateTime), "2016-09-28")]
        public DateTime Due { get; set; }
        [Display(Name = "Date de création")]
        public DateTime Creation { get; set; }
        [Display(Name = "Date terminée")]
        public DateTime? ClosedDate { get; set; }
        private bool _done;
        [Display(Name = "État")]
        public bool Done
        {
            get { return _done; }
            set
            {
                if (value)
                    ClosedDate = DateTime.Now;
                else
                    ClosedDate = null;
                _done = value;
            }
        }

        public static ValidationResult DateValide(Tache t)
        {
            return (t.Due > t.Creation)? ValidationResult.Success : new ValidationResult("La date de remise doit être postérieure à la date de création");
        }
    }
}