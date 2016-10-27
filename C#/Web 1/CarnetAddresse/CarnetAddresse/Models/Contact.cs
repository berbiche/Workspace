using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace CarnetAddresse.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le champ \"nom\" est requis!")]
        public string Nom { get; set; }
        [DisplayName("Téléphone")]
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        [DisplayName("Date de naissance")]
        public DateTime? DateNaissance { get; set; }
        [DisplayName("Code postal")]
        public string CodePostal { get; set; }

        public static List<Contact> GetList()
        {
            string contactManager = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;
            string requete = "SELECT * FROM Contact";

            using (SqlConnection cnx = new SqlConnection(contactManager))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cnx.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        List<Contact> contactList = new List<Contact>();
                        while (dataReader.Read())
                        {
                            Contact c = new Contact();
                            c.Id = (int)dataReader["id"];
                            c.Nom = (string)dataReader["Nom"];
                            c.Telephone = (string)dataReader["Telephone"];
                            c.Courriel = (string)dataReader["Courriel"];
                            c.CodePostal = (string)dataReader["CodePostal"];
                            if (!dataReader.IsDBNull(dataReader.GetOrdinal("DateNaissance")))
                            {
                                c.DateNaissance = (DateTime)dataReader["DateNaissance"];
                            }
                            contactList.Add(c);
                        }
                        return contactList;
                    }
                }
            }
        }

        public static bool Destroy(int id)
        {
            string cN = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;
            string requete = "DELETE FROM Contact WHERE Id = " + id;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                    return true;
                }
            }
        }
    }
}