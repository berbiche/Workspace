using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TP1.Models;

namespace TP2.Controllers
{
    public class AuthController : Controller
    {
        private const string Erreur = "~/Views/Shared/Error.cshtml";

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.SaveAsNew())
                    {
                        FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Create");
                }
                return View(Erreur);
            }
            catch
            {
                return View(Erreur);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password, string returnUrl = "")
        {
            ViewBag.error = string.Empty;
            ViewBag.ReturnUrl = returnUrl;

            if (password.Length < 8 && password.Length > 40 && !TP1.Models.User.IsValid(email, password))
            {
                ViewBag.error = "Le compte n'existe pas ou le mot de passe est invalide";
                return View();
            }

            FormsAuthentication.SetAuthCookie(email, false);

            if (returnUrl == "")
                return RedirectToAction("Index", "Task");

            return Redirect(returnUrl);
        }
        
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}