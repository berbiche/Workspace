using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TP2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        [DisplayName("Nom complet")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255, ErrorMessage = "Adresse courriel invalide")]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "La chaîne \"{0}\" doit comporter au moins {2} caractères.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required]
        [StringLength(40)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        [Display(Name = "Confirmer le mot de passe")]
        public string PasswordRepeated { get; set; }

        public bool SaveAsNew()
        {
            string cN = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;
            try
            {

                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    // Utilisation de la connexion
                    string requete = "INSERT INTO [User] (FullName, Email, Password) "
                                   + "OUTPUT INSERTED.ID "
                                   + "VALUES (@FullName, @Email, @Password)";

                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.Add("FullName", SqlDbType.VarChar);
                        cmd.Parameters.Add("Email", SqlDbType.VarChar);
                        cmd.Parameters.Add("Password", SqlDbType.NVarChar);

                        //donner des valeurs aux paramètres
                        cmd.Parameters["FullName"].SqlValue = this.FullName;
                        cmd.Parameters["Email"].SqlValue = this.Email;
                        cmd.Parameters["Password"].SqlValue = Encrypt(this.Password);

                        cnx.Open();
                        this.Id = (int)cmd.ExecuteScalar();
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

        public static bool IsValid(string email, string password)
        {
            string cN = ConfigurationManager.ConnectionStrings["TaskManager"].ConnectionString;

            try
            {

                using (SqlConnection cnx = new SqlConnection(cN))
                {
                    string requete = "SELECT * FROM [User] WHERE Email = @email";
                    using (SqlCommand cmd = new SqlCommand(requete, cnx))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add("Email", SqlDbType.VarChar);
                        cmd.Parameters["Email"].SqlValue = email;
                        cnx.Open();
                        using (SqlDataReader dR = cmd.ExecuteReader())
                        {
                            if (!dR.HasRows)
                                return false;

                            dR.Read();

                            return (string)dR["Password"] == Encrypt(password);
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static string Encrypt(string password)
        {
            return BitConverter.ToString(((HashAlgorithm)CryptoConfig.CreateFromName("MD5"))
                .ComputeHash(new UTF8Encoding().GetBytes(password))).Replace("-", "");
        }

    }
}