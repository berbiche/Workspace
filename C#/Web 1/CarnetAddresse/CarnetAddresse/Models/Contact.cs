using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Ajax.Utilities;

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
        [DataType(DataType.Date)]
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
                            Contact c = new Contact
                            {
                                Id = (int) dataReader["id"],
                                Nom = (string) dataReader["Nom"],
                                Telephone = (string) dataReader["Telephone"],
                                Courriel = (string) dataReader["Courriel"],
                                CodePostal = (string) dataReader["CodePostal"]
                            };
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
            string requete = "DELETE FROM Contact WHERE Id = @Id";

            try
            {

                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add("Id", SqlDbType.Int);
                        cmd.Parameters["Id"].SqlValue = id;
                        cnx.Open();
                        cmd.ExecuteNonQuery();
                        cnx.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static Contact FindOne(int id)
        {
            string cStr = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "SELECT * FROM Task WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("Id", SqlDbType.Int);
                    cmd.Parameters["Id"].SqlValue = id;
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        Contact c = new Contact();
                        while (dataReader.Read())
                        {
                            c.Id = (int)dataReader["Id"];
                            c.Nom = (string)dataReader["Nom"];
                            c.Courriel = (string)dataReader["Courriel"];
                            c.Telephone = (string)dataReader["Telephone"];
                            c.CodePostal = (string)dataReader["CodePostal"];
                            if (!dataReader.IsDBNull(dataReader.GetOrdinal("DateNaissance")))
                            {
                                c.DateNaissance = (DateTime)dataReader["DateNaissance"];
                            }
                        }
                        return c;
                    }
                }
            }
        }

        public static List<Contact> FindName(string nom)
        {
            string cStr = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "SELECT * FROM Contact WHERE Nom Like @Nom";

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Add("Nom", SqlDbType.VarChar);
                    cmd.Parameters["Nom"].SqlValue = nom;
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        List<Contact> contactList = new List<Contact>();
                        while (dataReader.Read())
                        {
                            Contact c = new Contact
                            {
                                Id = (int) dataReader["Id"],
                                Nom = (string) dataReader["Nom"],
                                Courriel = (string) dataReader["Courriel"],
                                Telephone = (string) dataReader["Telephone"],
                                CodePostal = (string) dataReader["CodePostal"]
                            };
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

        public bool SaveAsNew()
        {
            string cN = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                // Utilisation de la connexion
                string requete = "INSERT INTO Contact (Nom, Telephone, Courriel"
                                + ", DateNaissance, CodePostal) VALUES (@Nom, @Telephone"
                                + ", @Courriel, @DateNaissance, @CodePostal);";

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    //définir les paramètres
                    cmd.Parameters.Add("Nom", SqlDbType.VarChar);
                    cmd.Parameters.Add("Telephone", SqlDbType.VarChar);
                    cmd.Parameters.Add("Courriel", SqlDbType.VarChar);
                    cmd.Parameters.Add("CodePostal", SqlDbType.VarChar);
                    cmd.Parameters.Add("DateNaissance", SqlDbType.DateTime2);

                    //donner des valeurs aux paramètres
                    cmd.Parameters["Nom"].SqlValue = this.Nom;
                    cmd.Parameters["Telephone"].SqlValue = this.Telephone?? "";
                    cmd.Parameters["Courriel"].SqlValue = this.Courriel?? "";
                    cmd.Parameters["CodePostal"].SqlValue = this.CodePostal?? "";
                    if (this.DateNaissance.HasValue)
                        cmd.Parameters["DateNaissance"].SqlValue = this.DateNaissance;
                    else
                        cmd.Parameters["DateNaissance"].SqlValue = DBNull.Value;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();

                    return true;
                }
            }
        }

        public bool Update()
        {
            string cN = ConfigurationManager.ConnectionStrings["ContactManager"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cN))
            {
                // Utilisation de la connexion
                string requete = "UPDATE Task SET"
                                + "Nom=@Nom,"
                                + "Telephone=@Telephone,"
                                + "Courriel=@Courriel,"
                                + "CodePostal=@CodePostal,"
                                + "DateNaissance=@DateNaissance "
                                + "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    //définir les paramètres
                    cmd.Parameters.Add("Id", SqlDbType.Int);
                    cmd.Parameters.Add("Nom", SqlDbType.VarChar);
                    cmd.Parameters.Add("Telephone", SqlDbType.VarChar);
                    cmd.Parameters.Add("Courriel", SqlDbType.VarChar);
                    cmd.Parameters.Add("CodePostal", SqlDbType.VarChar);
                    cmd.Parameters.Add("DateNaissance", SqlDbType.DateTime2);

                    //donner des valeurs aux paramètres
                    cmd.Parameters["Id"].SqlValue = this.Id;
                    cmd.Parameters["Nom"].SqlValue = this.Nom;
                    cmd.Parameters["Telephone"].SqlValue = this.Telephone;
                    cmd.Parameters["Courriel"].SqlValue = this.Courriel;
                    cmd.Parameters["CodePostal"].SqlValue = this.CodePostal;
                    cmd.Parameters["DateNaissance"].SqlValue = this.DateNaissance;

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();

                    return true;
                }
            }
        }
    }
}