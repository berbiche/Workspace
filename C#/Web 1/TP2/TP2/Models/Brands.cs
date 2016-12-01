using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace TP2.Models
{
    public class Brands
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ \"{0}\" est requis")]
        [Display(Name = "Nom")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Description { get; set; }

        [Display(Name = "Site Web")]
        [DataType(DataType.Url)]
        [StringLength(1000, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Website { get; set; }

        [Display(Name = "Adresse")]
        [DataType(DataType.PostalCode)]
        [StringLength(400, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Address { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(17, ErrorMessage = "Le champ \"{0}\" ne doit pas être plus long que {1} caractères")]
        public string Phone { get; set; }

        public static List<Brands> GetList()
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;
            string requete = "SELECT * FROM [toys4us_Brands]";

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        List<Brands> brandsList = new List<Brands>();
                        while (dataReader.Read())
                        {
                            brandsList.Add(new Brands
                            {
                                Id = (int)dataReader["id"],
                                Name = (string)dataReader["Name"],
                                Description = dataReader["Description"] as string ?? "",
                                Address = dataReader["Address"] as string ?? "",
                                Phone = dataReader["Phone"] as string ?? "",
                                Website = dataReader["Website"] as string ?? ""
                            });
                        }
                        return brandsList;
                    }
                }
            }
        }

        public static bool Destroy(int id)
        {
            string cN = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;
            string requete = "DELETE FROM [toys4us_Brands] WHERE Id = " + id;

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

        public static Brands FindOne(int id)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            try
            {
                using (SqlConnection cnx = new SqlConnection(cStr))
                {
                    string requete = "SELECT * FROM [toys4us_Brands] WHERE Id =" + id;
                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cnx.Open();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            dataReader.Read();
                            return new Brands
                            {
                                Id = (int) dataReader["id"],
                                Name = (string) dataReader["Name"],
                                Description = dataReader["Description"] as string ?? "",
                                Address = dataReader["Address"] as string ?? "",
                                Phone = dataReader["Phone"] as string ?? "",
                                Website = dataReader["Website"] as string ?? ""
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public bool SaveAsNew()
        {
            string cN = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            try
            {
                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    // Utilisation de la connexion
                    string requete = "INSERT INTO [toys4us_Brands] (Name, Description, Website"
                                     + ", Address, Phone) VALUES (@Name, @Description"
                                     + ", @Website, @Address, @Phone);";

                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        //définir les paramètres
                        cmd.Parameters.Add("Name", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Website", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Address", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Phone", SqlDbType.VarChar);

                        //donner des valeurs aux paramètres
                        cmd.Parameters["Name"].SqlValue = this.Name;
                        cmd.Parameters["Description"].SqlValue = this.Description ?? (object)DBNull.Value;
                        cmd.Parameters["Website"].SqlValue = this.Website ?? (object)DBNull.Value;
                        cmd.Parameters["Address"].SqlValue = this.Address ?? (object)DBNull.Value;
                        cmd.Parameters["Phone"].SqlValue = this.Phone ?? (object)DBNull.Value;

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


        public bool Update()
        {
            string cN = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            try
            {
                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    // Utilisation de la connexion
                    string requete = "UPDATE [toys4us_Brands] SET "
                        + "Name=@Name,"
                        + "Description=@Description,"
                        + "Website=@Website,"
                        + "Address=@Address,"
                        + "Phone=@Phone,"
                        + "WHERE Id = " + this.Id;

                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        //définir les paramètres
                        cmd.Parameters.Add("Name", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Website", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Address", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Phone", SqlDbType.VarChar);

                        //donner des valeurs aux paramètres
                        cmd.Parameters["Name"].SqlValue = this.Name;
                        cmd.Parameters["Description"].SqlValue = this.Description;
                        cmd.Parameters["Website"].SqlValue = this.Website;
                        cmd.Parameters["Address"].SqlValue = this.Address;
                        cmd.Parameters["Phone"].SqlValue = this.Phone;

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

    }
}