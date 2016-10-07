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
        private PersistentList<Tache> depot = new PersistentList<Tache>();
        private const string Erreur = "~/Views/Shared/Error.cshtml";

        // GET: Task
        public ActionResult Index()
        {
            return View(depot);
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            Tache toDetails = depot.Find(p => p.Id == id);
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
                if (ModelState.IsValid)
                {
                    newTache.Creation = DateTime.Now;
                    if (newTache.Creation.CompareTo(newTache.Due) == 1)
                        return View(Erreur);
                    depot.Add(newTache);
                    depot.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(new Tache());
            }
            catch
            {
                return View(Erreur);
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            Tache toEdit = depot.Find(x => x.Id == id);
            return View(toEdit);
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tache newTache)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tache oldTache = depot.Find(p => p.Id == id);
                    int index = depot.IndexOf(oldTache);
                    newTache.Creation = oldTache.Creation;
                    depot[index] = newTache;
                    depot.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(Erreur);
            }
            catch
            {
                return View(Erreur);
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            Tache toDeleteTache = depot.Find(p => p.Id == id);
            return View(toDeleteTache);
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Tache t)
        {
            try
            {
                // TODO: Add delete logic here
                depot.RemoveAll(p => p.Id == id);
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(Erreur);
            }
        }

        [HttpPost]
        public ActionResult Terminate(int id)
        {
            try
            {
                depot.Find(p => p.Id == id).Done = true;
                depot.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(Erreur);
            }
        }
    }
}