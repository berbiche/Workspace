using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using TP1.Models;

namespace TP1.Controllers
{
    public class TaskController : Controller
    {
        private const string Erreur = "~/Views/Shared/Error.cshtml";

        [Authorize]
        public ActionResult Index()
        {
            return View(Task.GetList(User.Identity.Name));
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View(Task.FindOne(id));
        }

        [Authorize]
        public ActionResult Create()
        {
            Task emptyTache = new Task();
            return View(emptyTache);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Task newTask)
        {
            if (ModelState.IsValid)
                if (newTask.SaveAsNew())
                    return RedirectToAction("Index");
            return View(Erreur);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View(Task.FindOne(, id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            if (ModelState.IsValid)
                if (task.Update())
                    return RedirectToAction("Index");
            return View(Erreur);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            return View(Task.FindOne(id));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(int id, Task t)
        {
            if (Task.Destroy(id))
                return RedirectToAction("Index");
            return View(Erreur);
        }

        [Authorize]
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