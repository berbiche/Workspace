using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace TP2.Models
{
    public enum Genders
    {
        [Display(Name = "Unisexe", ShortName = "Unisexe", Description = "Unisexe")]
        O = 0,
        [Display(Name = "Garçon", ShortName = "Garçon", Description = "Garçon")]
        M = 1,
        [Display(Name = "Fille", ShortName = "Fille", Description = "Fille")]
        F = 2
    }

    public class Toys
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom")]
        [StringLength(200, ErrorMessage = "Le nom du jouet ne peut pas dépasser {0} caractères")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, ErrorMessage = "La description du jouet ne peut pas dépasser {0} caractères")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Prix")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        [Range(0.0, 214748.3647)] //la limite de smallmoney
        public decimal Price { get; set; }

        [Display(Name = "Date d'ajout")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Sexe")]
        public Genders Gender { get; set; }

        [Display(Name = "# de compagnie")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        public int? BrandId { get; set; }

        //[Display(Name = "Catégorie d'âge")]
        //public AgeCategories Age { get; set; }

        //[Display(Name = "Catégorie de jouet")]
        //public Categories Category { get; set; }

        public string GetGender
        {
            get { return this.Gender.GetType().GetProperty("Display").Name; }
        }

        public static List<Toys> GetList()
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;
            List<Toys> toysList = new List<Toys>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(cStr))
                {
                    string requete = "SELECT * FROM [toys4us_Toys]";
                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cnx.Open();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                Toys toys = new Toys();
                                toys.Id = (int)dataReader["id"];
                                toys.DateAdded = (DateTime)dataReader["Date_Added"];
                                toys.Description = (string)dataReader["Description"];
                                toys.Name = (string)dataReader["Name"];
                                toys.Price = (decimal)dataReader["Price"];
                                toys.BrandId = dataReader["Brand"] as int? ?? null;
                                toys.Gender = (Genders)Enum.Parse(typeof(Genders), (string)dataReader["Gender"]);
                                toysList.Add(toys);
                            }
                            return toysList;
                        }
                    }
                }
            }
            catch
            {
                return toysList;
            }
        }

        public static bool Destroy(int id)
        {
            string cN = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            try
            {
                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    string requete = "DELETE FROM [toys4us_Toys] WHERE Id = " + id;
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
            catch
            {
                return false;
            }
        }

        public static Toys FindOne(int id)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            try
            {
                using (SqlConnection cnx = new SqlConnection(cStr))
                {
                    string requete = "SELECT * FROM [toys4us_Toys] WHERE Id =" + id;
                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cnx.Open();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            dataReader.Read();
                            return new Toys
                            {
                                DateAdded = (DateTime)dataReader["Date_Added"],
                                Id = (int)dataReader["id"],
                                Description = (string)dataReader["Description"],
                                Name = (string)dataReader["Name"],
                                Price = (decimal)dataReader["Price"],
                                BrandId = dataReader["Brand"] as int? ?? null,
                                Gender = (Genders)Enum.Parse(typeof(Genders), (string)dataReader["Gender"])
                            };
                        }
                    }
                }
            }
            catch
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
                    string requete = "INSERT INTO [toys4us_Toys] (Name, Description, Price"
                                     + ", Date_Added, Gender, Brand) VALUES (@Name, @Description"
                                     + ", @Price, @DateAdded, @Gender, @BrandId);";

                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        //définir les paramètres
                        cmd.Parameters.Add("Name", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Price", SqlDbType.SmallMoney);
                        cmd.Parameters.Add("DateAdded", SqlDbType.Date);
                        cmd.Parameters.Add("Gender", SqlDbType.Char);
                        cmd.Parameters.Add("BrandId", SqlDbType.Int);

                        //donner des valeurs aux paramètres
                        cmd.Parameters["Name"].SqlValue = this.Name;
                        cmd.Parameters["Description"].SqlValue = this.Description;
                        cmd.Parameters["Price"].SqlValue = this.Price;
                        cmd.Parameters["DateAdded"].SqlValue = this.DateAdded;
                        cmd.Parameters["Gender"].SqlValue = this.Gender.ToString();
                        if (this.BrandId != null)
                            cmd.Parameters["BrandId"].SqlValue = this.BrandId;
                        else
                            cmd.Parameters["BrandId"].SqlValue = DBNull.Value;

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
                    string requete = "UPDATE [toys4us_Toys] SET "
                        + "Name=@Name,"
                        + "Description=@Description,"
                        + "Price=@Price,"
                        + "Date_Added=@DateAdded,"
                        + "Gender=@Gender,"
                        + "Brand=@BrandId "
                        + "WHERE Id = " + this.Id;

                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        //définir les paramètres
                        cmd.Parameters.Add("Name", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Description", SqlDbType.NVarChar);
                        cmd.Parameters.Add("Price", SqlDbType.SmallMoney);
                        cmd.Parameters.Add("DateAdded", SqlDbType.Date);
                        cmd.Parameters.Add("Gender", SqlDbType.Char);
                        cmd.Parameters.Add("BrandId", SqlDbType.Int);

                        //donner des valeurs aux paramètres
                        cmd.Parameters["Name"].SqlValue = this.Name;
                        cmd.Parameters["Description"].SqlValue = this.Description;
                        cmd.Parameters["Price"].SqlValue = this.Price;
                        cmd.Parameters["DateAdded"].SqlValue = this.DateAdded;
                        cmd.Parameters["Gender"].SqlValue = this.Gender.ToString();
                        if (this.BrandId != null)
                            cmd.Parameters["BrandId"].SqlValue = this.BrandId;
                        else
                            cmd.Parameters["BrandId"].SqlValue = DBNull.Value;

                        cnx.Open();
                        cmd.ExecuteNonQuery();
                        cnx.Close();

                        return true;
                    }
                }
            }
            catch (SqlException e)
            {
                return false;
            }
        }


    }
}