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
        [Display(Name = "Garçon")]
        M,
        [Display(Name = "Fille")]
        F,
        [Display(Name = "Unisexe")]
        O
    }

    public class Toys
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nom du jouet")]
        [StringLength(200, ErrorMessage = "Le nom du jouet ne peut pas dépasser {0} caractères")]
        public string Name { get; set; }

        [Display(Name = "Description du jouet")]
        [DataType(DataType.MultilineText)]
        [StringLength(400, ErrorMessage = "La description du jouet ne peut pas dépasser {0} caractères")]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        [Display(Name = "Prix")]
        [Range(0.0, 214748.3647)] //la limite de smallmoney
        public double Price { get; set; }

        [Display(Name = "Date d'ajout")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        [Required]
        [DataType(DataType.Custom)]
        [Display(Name = "Sexe")]
        public Genders Gender { get; set; }

        [Display(Name = "Compagnie")]
        [DisplayFormat(NullDisplayText = "Aucune compagnie associée")]
        public int? BrandId { get; set; }

        //[Display(Name = "Catégorie d'âge")]
        //public AgeCategories Age { get; set; }

        //[Display(Name = "Catégorie de jouet")]
        //public Categories Category { get; set; }

        public static List<Toys> GetList()
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;
            string requete = "SELECT * FROM [toys4us_Toys]";

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        List<Toys> toysList = new List<Toys>();
                        while (dataReader.Read())
                        {
                            Toys toy = new Toys();
                            toy.Id = (int)dataReader["id"];
                            toy.Description = (string)dataReader["Description"];
                            toy.Name = (string)dataReader["Name"];
                            toy.Price = (double)dataReader["Price"];
                            toy.DateAdded = (DateTime)dataReader["Date_Added"];
                            toy.BrandId = (int)dataReader["Brand"];
                            toy.Gender = (Genders)dataReader["Gender"];
                            toysList.Add(toy);
                        }
                        return toysList;
                    }
                }
            }
        }

        public static bool Destroy(int id)
        {
            string cN = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;
            string requete = "DELETE FROM [toys4us_Toys] WHERE Id = " + id;

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

        public static Toys FindOne(int id)
        {
            string cStr = ConfigurationManager.ConnectionStrings["Toys4Us"].ConnectionString;

            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                string requete = "SELECT * FROM [toys4us_Toys] WHERE Id =" + id;
                using (SqlCommand cmd = new SqlCommand(requete, cnx))
                {
                    cnx.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        Toys toy = new Toys();
                        while (dataReader.Read())
                        {
                            toy.Id = (int)dataReader["id"];
                            toy.Description = (string)dataReader["Description"];
                            toy.Name = (string)dataReader["Name"];
                            toy.Price = (double)dataReader["Price"];
                            toy.DateAdded = (DateTime)dataReader["Date_Added"];
                            toy.BrandId = (int)dataReader["Brand"];
                            toy.Gender = (Genders)dataReader["Gender"];
                        }
                        return toy;
                    }
                }
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
                                     + ", DateAdded, Gender, Brand) VALUES (@Name, @Description"
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
                        + "Brand=@BrandId,"
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
            catch
            {
                return false;
            }
        }


    }
}