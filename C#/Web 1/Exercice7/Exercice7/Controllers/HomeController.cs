using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercice7.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/DepotFichiers/"));
            return View(dirInfo.GetFiles());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fichier)
        {
            if (fichier.ContentLength > 0)
            {
                string nomComplet = Server.MapPath("~/DepotFichiers/" + fichier.FileName);
                fichier.SaveAs(nomComplet);
            }
            return RedirectToAction("Index");
        }

        public FileResult Telecharger(string fichier)
        {
            try
            {
                string nomComplet = Path.Combine(Server.MapPath("~/DepotFichiers"), fichier);
                return File(nomComplet, "application/octet-stream", fichier);
            }
            catch
            {
                return null;
            }
        }

    }
}