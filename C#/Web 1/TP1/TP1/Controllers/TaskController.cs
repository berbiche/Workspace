using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP1.Models;

namespace TP1.Controllers
{
    public class TaskController : Controller
    {
        PersistentList<Tache> depot = new PersistentList<Tache>();

        // GET: Task
        public ActionResult Index()
        {
            return View(depot);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            Tache toDetails = depot.Find(p => p.id == id);
            return View(toDetails);
        }

        // GET: Default/Create
        public ActionResult Create()
        {
            Tache emptyTache = new Tache();
            return View(emptyTache);
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(Tache newTache)
        {
            try
            {
                depot.Add(newTache);
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            Tache toEdit = depot.Find(x => x.id == id);
            return View(toEdit);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tache newTache)
        {
            try
            {
                Tache oldTache = depot.Find(p => p.id == id);
                int index = depot.IndexOf(oldTache);
                depot[index] = newTache;
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Shared/Erreur.cshtml");
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}