using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarnetAddresse.Models
{
    public class Contact
    {
        [Key]
        public int id { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string CodePostal { get; set; }
        //public Contact()
        //{

        //}
    }
}