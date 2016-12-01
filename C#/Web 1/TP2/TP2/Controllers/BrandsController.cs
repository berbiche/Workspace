using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP2.Models;

namespace TP2.Controllers
{
    [Authorize]
    public class BrandsController : Controller
    {

        // GET: Brands
        public ActionResult Index()
        {
            return View(Brands.GetList());
        }

        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = Brands.FindOne(id.Value);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brands brands)
        {
            if (ModelState.IsValid)
            {
                if (brands.SaveAsNew())
                    return RedirectToAction("Index");
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View(brands);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = Brands.FindOne((int)id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brands brands)
        {
            if (ModelState.IsValid)
            {
                brands.Update();
                return RedirectToAction("Index");
            }
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View(brands);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = Brands.FindOne(id.Value);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Brands.Destroy(id))
                return RedirectToAction("Index");
            ViewBag.Error = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors));
            return View();
        }

    }
}
