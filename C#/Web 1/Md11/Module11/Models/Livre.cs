using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module11.Models
{
    public class Livre
    {
        public Guid IdLivre { get; set; }
        public String Nom { get; set; }
        public DateTime  DateParution { get; set; }
    }
}