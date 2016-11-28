﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using TP2.Models;

namespace TP2.Controllers
{
    [Authorize]
    public class ToysController : Controller
    {

        public ActionResult Index()
        {
            return View(Toys.GetList());
        }

        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toys toys = Toys.FindOne(id.Value);
            if (toys == null)
                return HttpNotFound();
            return View(toys);
        }

        public ActionResult Create()
        {
            Toys emptyToy = new Toys();
            return View(emptyToy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Toys newToy)
        {
            if (ModelState.IsValid)
                if (newToy.SaveAsNew())
                    return RedirectToAction("Index");
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toys toys = Toys.FindOne(id.Value);
            if (toys == null)
                return HttpNotFound();
            return View(toys);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Toys toy)
        {
            if (ModelState.IsValid)
                if (toy.Update())
                    return RedirectToAction("Index");
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Toys toys = Toys.FindOne(id.Value);
            if (toys == null)
                return HttpNotFound();
            return View(toys);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Toys.Destroy(id))
                return RedirectToAction("Index");
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View();
        }
    }
}