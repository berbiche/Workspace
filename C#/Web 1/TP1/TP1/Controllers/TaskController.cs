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
        private const string Erreur = "~/Views/Shared/Error.cshtml";

        // GET: Task
        public ActionResult Index()
        {
            return View(Task.GetList());
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View(Task.FindOne(id));
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            Task emptyTache = new Task();
            return View(emptyTache);
        }

        // POST: Task/Create
        [HttpPost]
        public ActionResult Create(Task newTask)
        {
            if (ModelState.IsValid)
                if (newTask.SaveAsNew())
                    return RedirectToAction("Index");
            return View(Erreur);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            return View(Task.FindOne(id));
        }

        // POST: Task/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            if (ModelState.IsValid)
                if (task.Update())
                    return RedirectToAction("Index");
            return View(Erreur);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View(Task.FindOne(id));
        }

        // POST: Task/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Task t)
        {
            if (Task.Destroy(id))
                return RedirectToAction("Index");
            return View(Erreur);
        }

        // POST: Task/Terminate/5
        [HttpPost]
        public ActionResult Terminate(int id)
        {
            Task asd = Task.FindOne(id);
            asd.Done = true;
            if (asd.Update())
                return RedirectToAction("Index");
            return View(Erreur);
        }
    }
}