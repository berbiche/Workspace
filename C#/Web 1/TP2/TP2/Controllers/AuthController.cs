using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TP2.Models;

namespace TP2.Controllers
{
    public class AuthController : Controller
    {

        [HttpGet]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.SaveAsNew())
                {
                    FormsAuthentication.SetAuthCookie(user.Name, false);
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }
            ViewBag.Error = "Une erreur est survenue en essayant de créer l'utilisateur.\nVeuillez réessayer.";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password, string returnUrl = "")
        {
            ViewBag.error = string.Empty;
            ViewBag.ReturnUrl = returnUrl;

            if (password.Length < 8 || password.Length > 40 || Models.User.IsValid(email, password))
            {
                ViewBag.Error = "Le compte n'existe pas ou le mot de passe est invalide";
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