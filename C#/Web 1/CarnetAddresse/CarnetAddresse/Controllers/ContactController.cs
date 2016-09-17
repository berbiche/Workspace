﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarnetAddresse.Models;

namespace CarnetAddresse.Controllers
{
    public class ContactController : Controller
    {
        PersistentList<Contact> depot = new PersistentList<Contact>();

        // GET: Contact
        public ActionResult Index(string q = null)
        {
            if (q != null)
            {
				ViewBag.q = true;
                return View(depot.FindAll(p => p.Nom.Equals(q)));
            }
            return View(depot);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            Contact toEdit = depot.Find(p => p.id == id);
            return View(toEdit);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            Contact emptyContact = new Contact();
            return View(emptyContact);
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(Contact newContact)
        {
            try
            {
                // On ajoute le nouveau contact en mémoire (i.e. dans la liste)
                depot.Add(newContact);
                // On sauvegarde la liste ainsi modifiée
                depot.SaveChanges();
                // On affiche un message
                return Content("Le contact a été ajouté<br><a href=\"/Contact\">Back to List</a>");
            }
            catch
            {
                return Content("Une erreur s'est produite!<br><a href=\"/Contact\">Back to List</a>");
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            // On va chercher dans le dépôt le contact à modifier
            Contact toEdit = depot.Find(p => p.id == id);
            // Appel à la vue avec le contact comme paramètre
            return View(toEdit);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Contact newContact)
        {
            try
            {
                // On cherche le contact à modifier et on le remplace
                Contact oldContact = depot.Find(p => p.id == id);
                int index = depot.IndexOf(oldContact);
                depot[index] = newContact;
                // Enregistrement en bonne et due forme!
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Erreur");
            }
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            // On va chercher dans le dépôt le contact à supprimer
            Contact toDelete = depot.Find(p => p.id == id);
            // On retourne une vue et on associe le contact à la vue!
            return View(toDelete);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Contact c) //second parameter inutile ici
        {
            try
            {
                // On supprime de la liste
                depot.RemoveAll(p => p.id == id);
                // On sauvegarde la liste
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Erreur");
            }
        }
    }
}