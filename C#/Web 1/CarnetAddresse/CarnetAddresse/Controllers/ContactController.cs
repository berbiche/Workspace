using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarnetAddresse.Models;

namespace CarnetAddresse.Controllers
{
    public class ContactController : Controller
    {

        // GET: Contact
        public ActionResult Index(string q = null)
        {
            if (q != null)
            {
                
				ViewBag.q = true;
                return View(Contact.FindName(q));
            }
            return View(Contact.GetList());
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View(Contact.FindOne(id));
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
            if (ModelState.IsValid)
                if (newContact.SaveAsNew())
                    return RedirectToAction("Index");
            return Content("Erreur");
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Contact.FindOne(id));
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Contact newContact)
        {
            if (ModelState.IsValid)
                if (newContact.Update())
                    return RedirectToAction("Index");
            return Content("Erreur");
        }

        // GET: Contact/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(Contact.FindOne(id));
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, dynamic a)
        {
            if (Contact.Destroy(id))
                return RedirectToAction("Index");
            return Content("Erreur");
        }
    }
}
