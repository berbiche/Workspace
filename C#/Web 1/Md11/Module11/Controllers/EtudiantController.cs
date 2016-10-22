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
    public class EtudiantController : Controller
    {
        // GET: Etudiant
        public ActionResult Index()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            string requete = "SELECT * FROM Etudiant";
            List<Etudiant> listeEtudiants = new List<Etudiant>();
            using (SqlConnection conn = new SqlConnection(ConnMod11))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Etudiant e = new Etudiant();
                            e.IdEtudiant = (Guid)dr["IdEtudiant"];
                            e.Nom = (string)dr["Nom"];
                            e.NoDossier = (string)dr["NoDossier"];
                            listeEtudiants.Add(e);
                        }
                    }
                }
            }
            return View(listeEtudiants);
        }

        // GET: Etudiant/TestInsertEtudiant
        public ActionResult TestInsertEtudiant()
        {
            string ConnMod11 = ConfigurationManager.ConnectionStrings["ConnMod11"].ConnectionString;
            string requete = "Insert into Etudiant(IdEtudiant,Nom,NoDossier) Values ('3DD3C4B2-0314-4229-A77C-E6F1F25B793B','Nicolas Berbiche','1567925')";

            using (SqlConnection conn = new SqlConnection(ConnMod11))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(requete, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("Index");
        }
    }
}