using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string id)
        {
            id = id?.Trim();
            if (id.IsNullOrWhiteSpace())
                return View();

            ViewBag.titre = id;
            var article = Articles.Find(id);
            return View(article);
        }

        
        public ActionResult Ajouter(string id)
        {
            id = id?.Trim();
            return View(new Article() { Titre = id });
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Ajouter(Article article)
        {
            if (!ModelState.IsValid)
                return View(article);

            article.IdContributeur = 1;
            article.Revision = 1;
            article.DateModification = DateTime.Now;
            Articles.Add(article);
            return RedirectToAction("Index", "Home", new { id = article.Titre });
        }


        public ActionResult Modifier(string id)
        {
            if (id.IsNullOrWhiteSpace())
                return RedirectToAction("Index", "Home");

            Article article = Articles.Find(id);
            if (article == null)
                return RedirectToAction("Index", "Home");

            return View(article);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Modifier(Article article)
        {
            if (!ModelState.IsValid)
                return View(article);

            article.IdContributeur = 1;
            article.Revision++;
            article.DateModification = DateTime.Now;
            Articles.Update(article);
            return RedirectToAction("Index", "Home", new { id = article.Titre });
        }
        

        public ActionResult Supprimer(string id)
        {
            if (id.IsNullOrWhiteSpace())
                return RedirectToAction("index");

            Article article = Articles.Find(id);
            if (article == null)
                return RedirectToAction("index");

            return View(article);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupprimerConfirmer(string id)
        {
            Articles.Delete(id);
            return RedirectToAction("index");
        }
    }
}
