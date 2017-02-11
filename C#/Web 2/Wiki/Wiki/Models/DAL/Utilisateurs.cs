using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    public static class Utilisateurs
    {
        private static string ConnectionString => ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;

        public static void Add(Utilisateur u)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("AddUtilisateur", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("mdp", SqlDbType.NVarChar).SqlValue = Encrypt(u.Mdp);
                    commande.Parameters.Add("prenom", SqlDbType.NVarChar).SqlValue = u.Prenom;
                    commande.Parameters.Add("nomFamille", SqlDbType.NVarChar).SqlValue = u.NomFamille;
                    commande.Parameters.Add("courriel", SqlDbType.NVarChar).SqlValue = u.Courriel;
                    commande.Parameters.Add("langue", SqlDbType.NVarChar).SqlValue = u.Langue;

                    try
                    {
                        u.Id = Convert.ToInt32(commande.ExecuteScalar());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erreur d'ajout", e);
                    }
                }
            }
        }

        public static void Update(Utilisateur u)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("UpdateUtilisateur", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("prenom", SqlDbType.NVarChar).SqlValue = u.Prenom;
                    commande.Parameters.Add("nomFamille", SqlDbType.NVarChar).SqlValue = u.NomFamille;
                    commande.Parameters.Add("courriel", SqlDbType.NVarChar).SqlValue = u.Courriel;
                    commande.Parameters.Add("langue", SqlDbType.NVarChar).SqlValue = u.Langue;

                    try
                    {
                        commande.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erreur de modification", e);
                    }
                }
            }

        }

        public static bool Update(string courriel, string ancienMdP, string nouveauMdP)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("UpdateMotDePasse", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("Courriel", SqlDbType.NVarChar).SqlValue = courriel;
                    commande.Parameters.Add("ancienMDP", SqlDbType.NVarChar).SqlValue = Encrypt(ancienMdP);
                    commande.Parameters.Add("nouveauMDP", SqlDbType.NVarChar).SqlValue = Encrypt(nouveauMdP);

                    try
                    {
                        if (commande.ExecuteNonQuery() != 1)
                            return false;
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erreur de modification", e);
                    }
                }
            }
            return true;

        }

        public static void Remove(int id)
        {
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("DeleteUtilisateur", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

                    try
                    {
                        commande.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erreur de suppression", e);
                    }
                }
            }

        }


        public static Utilisateur FindByCourriel(string courriel)
        {
            Utilisateur u = null;
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("FindUtilisateurByCourriel", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("Courriel", SqlDbType.NVarChar).SqlValue = courriel;

                    //Traitement du DataReader
                    using (SqlDataReader dr = commande.ExecuteReader())
                    {
                        dr.Read();
                        u = ExtraireUtilisateur(dr);
                    }
                }
            }

            return u;
        }


        public static Utilisateur FindById(int id)
        {
            Utilisateur u = null;
            using (SqlConnection connexion = new SqlConnection(ConnectionString))
            {
                connexion.Open();

                //Création d'une commande
                using (SqlCommand commande = new SqlCommand("FindUtilisateurById", connexion))
                {
                    commande.CommandType = CommandType.StoredProcedure;
                    commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

                    //Traitement du DataReader
                    using (SqlDataReader dr = commande.ExecuteReader())
                    {
                        dr.Read();
                        u = ExtraireUtilisateur(dr);
                    }
                }
            }

            return u;
        }

        public static bool VerifyPasswordLogin(string courriel, string password)
        {
            Utilisateur u = FindByCourriel(courriel);
            return u != null && PasswordHash.ValidatePassword(password, u.Mdp);
        }

        private static string Encrypt(string password) => PasswordHash.CreateHash(password);

        private static Utilisateur ExtraireUtilisateur(IDataReader dr) =>
             new Utilisateur()
             {
                 Id = (int)dr["Id"],
                 Courriel = (string)dr["Courriel"],
                 Mdp = (string)dr["MDP"],
                 NomFamille = (string)dr["NomFamille"],
                 Prenom = (string)dr["Prenom"],
                 Langue = (string)dr["Langue"]
             };
    }
}