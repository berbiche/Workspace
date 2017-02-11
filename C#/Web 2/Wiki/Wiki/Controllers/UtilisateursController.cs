using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Wiki.Models.Views;
using Wiki.Models.Biz;
using Wiki.Models.DAL;

namespace Wiki.Controllers
{
    public class UtilisateursController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Profil()
        {
            try
            {
                Utilisateur user = Utilisateurs.FindByCourriel(User.Identity.Name);
                return View(new UserProfilViewModel()
                {
                    Courriel = user.Courriel,
                    Langue = user.Langue,
                    NomFamille = user.NomFamille,
                    Prenom = user.Prenom
                });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult Profil(UserProfilViewModel user)
        {
            try
            {
                Utilisateurs.Update(new Utilisateur()
                {
                    //si le courriel du formulaire égal celui utilisé dans le cookie
                    //par prévention et sécurité
                    Courriel = user.Courriel == User.Identity.Name ? user.Courriel : User.Identity.Name,
                    Langue = user.Langue,
                    NomFamille = user.NomFamille,
                    Prenom = user.Prenom
                });
                return RedirectToAction("Profil", "Utilisateurs");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }


        public ActionResult ChangerLangue(string langue, string returnUrl)
        {
            //if (!Utilisateur.Langues.Contains(langue))
            //{
            //    if (returnUrl.IsNullOrWhiteSpace())
            //        return RedirectToAction("Index", "Home");
            //    return Redirect(returnUrl);
            //}

            HttpCookie cookie = Request.Cookies["langue"];
            cookie = cookie ?? new HttpCookie("langue");

            cookie.Value = langue;
            Response.Cookies.Add(cookie);

            if (returnUrl.IsNullOrWhiteSpace())
                return RedirectToAction("Index", "Home");
            return Redirect(returnUrl);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeMdp(UserChangeMdpViewModel user)
        {
            if (!ModelState.IsValid || user.Courriel != User.Identity.Name)
                return RedirectToAction("Profil", "Utilisateurs");

            try
            {
                if (Utilisateurs.Update(user.Courriel, user.AncienMdp, user.NouveauMdp))
                    return RedirectToAction("Profil", "Utilisateurs");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult Inscription(string ReturnUrl = "")
        {
            //si la personne est déjà authentifiée
            if (User.Identity.IsAuthenticated)
            {
                if (!ReturnUrl.IsNullOrWhiteSpace())
                    return Redirect(ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            return View(new UserInscriptionViewModel() { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription(UserInscriptionViewModel user)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profil", "Utilisateurs");

            //Model invalide
            if (!ModelState.IsValid)
                return View(user);

            try
            {
                Utilisateurs.Add(new Utilisateur
                {
                    Courriel = user.Courriel,
                    Langue = user.Langue,
                    Mdp = user.Mdp,
                    Prenom = user.Prenom,
                    NomFamille = user.NomFamille
                });

                //Set le cookie, permanence décidé par l'utilisateur
                FormsAuthentication.SetAuthCookie(user.Courriel, user.Cookie);

                if (!user.ReturnUrl.IsNullOrWhiteSpace())
                    return Redirect(user.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                //le compte existe déjà ou un autre type d'erreur
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Connexion(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ReturnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", "Home");
                return Redirect(ReturnUrl);
            }

            return View(new UserLoginViewModel() { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion(UserLoginViewModel user)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profil", "Utilisateurs");

            if (!ModelState.IsValid)
                return View(user);

            if (!Utilisateurs.VerifyPasswordLogin(user.Courriel, user.Mdp))
                return View(user);

            //Le mot de passe est valide, il faut maintenant procéder à set le cookie
            FormsAuthentication.SetAuthCookie(user.Courriel, user.Cookie);

            //url de retour est nulle?
            if (user.ReturnUrl.IsNullOrWhiteSpace())
                return RedirectToAction("Index", "Home");

            return Redirect(user.ReturnUrl);
        }

        [Authorize]
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}