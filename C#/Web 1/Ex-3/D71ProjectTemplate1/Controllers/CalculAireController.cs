using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D71ProjectTemplate1.Controllers
{
	public class CalculAireController : Controller
	{
		// GET: CalculAire
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Cylindre(float rayon = 0, float hauteur = 0)
		{
			if ((rayon > 0) && (hauteur > 0))
			{
				ViewBag.volume = Math.PI * hauteur * Math.Pow(rayon, 2);
			}
			ViewBag.hauteur = hauteur.ToString();
			ViewBag.rayon = rayon.ToString();
			return View();
		}
	}
}