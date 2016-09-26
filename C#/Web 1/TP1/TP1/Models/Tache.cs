using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP1.Models
{
    public class Tache
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Client { get; set; }
        [Required, Range(1 , 3)]
        public int Priority { get; set; }
        [Required, Timestamp, CustomValidation(typeof(Tache), "DateValide")]
        public DateTime Due { get; set; }
        private DateTime creation = DateTime.Now;
        public DateTime Creation { get { return this.creation; } }
        private DateTime? closedDate;
        public DateTime? ClosedDate { get; }
        private bool done;
        public bool Done
        {
            get { return this.done; }
            set
            {
                if (value)
                    this.closedDate = DateTime.Now;
                else
                    this.closedDate = null;
                this.done = value;
            }
        }

        public static ValidationResult DateValide(Tache t)
        {
            if (t.Due < t.creation)
                return new ValidationResult("La date de remise doit être postérieur à la date de création");
            return ValidationResult.Success;
        }
    }
}