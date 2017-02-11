using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Collections.Generic;
using Wiki.Models.DAL;
using Wiki.Models.Biz;

namespace Wiki.Models.DAL
{
    // Class rendu static par nécessité
    public static class Articles
    {
        // Auteurs: Jean-Michel Michaud, Karl Dézainde, Andrée-Anne Roy, Renaud Roy
        public static int Add(Article a)
        {
            int idArticle;

            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                cnx.Open();

                //Création d'une commande
                SqlCommand commande = new SqlCommand("AddArticle", cnx);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Titre", SqlDbType.NVarChar).SqlValue = a.Titre;
                commande.Parameters.Add("Contenu", SqlDbType.NChar).SqlValue = a.Contenu;
                commande.Parameters.Add("IdContributeur", SqlDbType.NVarChar).SqlValue = a.IdContributeur;

                try
                {
                    idArticle = commande.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    idArticle = 0;
                }
            }
            return idArticle;
        }

        // Auteurs: Equipe find : charle-Antoinee/Andree/Jf/crys
        public static Article Find(string titre)
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("FindArticle", cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;
                try
                {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    Article a = null;


                    while (dataReader.Read())
                    {
                        a = new Article
                        {
                            IdContributeur = (int) dataReader["IdContributeur"],
                            Revision = (int) dataReader["Revision"],
                            Contenu = (string) dataReader["Contenu"],
                            Titre = (string) dataReader["Titre"],
                            DateModification = (DateTime) dataReader["DateModification"]
                        };

                    }
                    dataReader.Close();
                    return a;
                }
                finally
                {
                    cnx.Close();
                }

            }
        }


        // Auteurs: Vincent Simard, Phan Ngoc Long Denis, Floyd, Pierre-Olivier Morin
        public static IList<string> GetTitres()
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionString))
            {
                string requete = "GetTitresArticles"; //StoredProc.
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cnx.Open();

                try
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    IList<string> ListeTitre = new List<string>();
                    while (dataReader.Read())
                    {
                        string t = (string)dataReader["Titre"];
                        ListeTitre.Add(t);
                    }
                    dataReader.Close();

                    return ListeTitre;
                }
                finally
                {
                    cnx.Close();
                }
            }
        }

        // Auteurs: Alexandre, Vincent, William et Nicolas
        public static IList<Article> GetArticles()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("GetArticles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                try
                {
                    var dataReader = cmd.ExecuteReader();
                    var articles = new List<Article>();

                    while (dataReader.Read())
                    {
                        Article article = new Article();

                        article.Titre = (string) dataReader["Titre"];
                        article.Contenu = (string) dataReader["Contenu"];
                        article.Revision = (int) dataReader["Revision"];
                        article.IdContributeur = (int) dataReader["IdContributeur"];
                        article.DateModification = (DateTime) dataReader["DateModification"];

                        articles.Add(article);
                    }

                    return articles;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        
        // Auteurs: Rafael, Marcelo, Sebastien, Phu Lam
        public static int Update(Article a)
        {
            int modifiedLine = 0;

            using (SqlConnection connect = new SqlConnection(ConnectionString))
            {
                connect.Open();

                SqlCommand command = new SqlCommand("UpdateArticle", connect);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("Titre", SqlDbType.NVarChar).SqlValue = a.Titre;
                command.Parameters.Add("Contenu", SqlDbType.NVarChar).SqlValue = a.Contenu;
                command.Parameters.Add("IdContributeur", SqlDbType.Int).SqlValue = a.IdContributeur;

                try
                {
                    modifiedLine = command.ExecuteNonQuery();
                    return modifiedLine;
                }
                catch (Exception)
                {
                    return modifiedLine;
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        // Auteurs: Farouk Naamane , Jonathan Joseph Alcidor , Hazem Allouche , Jean Auguste
        public static int Delete(string titre)
        {
            using (SqlConnection conx = new SqlConnection(ConnectionString))
            {


                SqlCommand cmd = new SqlCommand("DeleteArticle", conx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("Titre", SqlDbType.NVarChar).Value = titre;
                cmd.Connection = conx;

                conx.Open();
                try
                {
                    int x = cmd.ExecuteNonQuery();
                    return x;
                }
                catch
                {
                    return 0;
                }
                finally
                {
                    conx.Close();
                }
            }
        }


        private static string ConnectionString => ConfigurationManager.ConnectionStrings["Wiki"].ConnectionString;
    }
}