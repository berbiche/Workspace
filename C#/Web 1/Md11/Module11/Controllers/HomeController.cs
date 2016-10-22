using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Module11.Models;

namespace Module11.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnMod11);
            string requete = "SELECT * FROM Livre";
            SqlCommand cmd = new SqlCommand(requete, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            List<Livre> listeLivre = new List<Livre>();
            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
               

                while (dr.Read())
                {
                    Livre n = new Livre();
                    n.IdLivre = (Guid)dr["IdLivre"];
                    n.Nom = (string)dr["Titre"];
                    n.DateParution = (DateTime)dr["DateParution"];
                    listeLivre.Add(n);
                }
                dr.Close();
            }
            finally
            {
                conn.Close();
            }
            //return Content("");
            return View(listeLivre);


            
        }
        public ActionResult TestInsertLivre()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnMod11);
            string requete = "INSERT INTO Livre (IdLivre,Titre,DateParution) VALUES ('4676C42F-E236-4298-8C92-F27E7F734A29', 'MVC 3', '2001-01-01')";
            SqlCommand cmd = new SqlCommand(requete, conn);
            cmd.CommandType = System.Data.CommandType.Text;

            try {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally {
                conn.Close();
            }
            return Content("");
        }

        public ActionResult TestInsertLivre2()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnMod11);
            string requete = "INSERT INTO Livre (IdLivre,Titre,DateParution) VALUES (@IdLivre, @Titre, @DateParution)";
            SqlCommand cmd = new SqlCommand(requete, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            //définir les paramètres
            cmd.Parameters.Add("IdLivre", SqlDbType.UniqueIdentifier);
            cmd.Parameters.Add("Titre", SqlDbType.NVarChar);
            cmd.Parameters.Add("DateParution", SqlDbType.Date);
            //donner des valeurs aux paramètres
            cmd.Parameters["IdLivre"].SqlValue = Guid.NewGuid();
            cmd.Parameters["Titre"].SqlValue = "MVC 4";
            DateTime? maDate = null;
            cmd.Parameters["DateParution"].SqlValue = maDate.HasValue ? (object)maDate.Value : DBNull.Value;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
            return Content("");
        }

        public ActionResult TestListLivre()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnMod11);
            string requete = "SELECT * FROM Livre";
            SqlCommand cmd = new SqlCommand(requete, conn);
            cmd.CommandType = System.Data.CommandType.Text;

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                List<Livre> listeLivre = new List<Livre>();

                while (dr.Read())
                {
                    Livre n = new Livre();
                    n.IdLivre = (Guid)dr["IdLivre"];
                    n.Nom = (string)dr["Titre"];
                    n.DateParution = (DateTime)dr["DateParution"];
                    listeLivre.Add(n);
                }
                dr.Close();
            }
            finally
            {
                conn.Close();
            }
            //return Content("");
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}