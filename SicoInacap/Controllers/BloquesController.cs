using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SicoInacap.Models;

namespace SicoInacap.Controllers
{
    public class BloquesController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Bloques
        public ActionResult Index()
        {
            return View(db.Bloque.ToList());
        }

        // GET: Bloques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloque bloque = db.Bloque.Find(id);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return View(bloque);
        }

        // GET: Bloques/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bloques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,HoraInicio,HoraTermino")] Bloque bloque)
        {
            if (ModelState.IsValid)
            {
                db.Bloque.Add(bloque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bloque);
        }

        // GET: Bloques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloque bloque = db.Bloque.Find(id);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return View(bloque);
        }

        // POST: Bloques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,HoraInicio,HoraTermino")] Bloque bloque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloque);
        }

        // GET: Bloques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bloque bloque = db.Bloque.Find(id);
            if (bloque == null)
            {
                return HttpNotFound();
            }
            return View(bloque);
        }

        // POST: Bloques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bloque bloque = db.Bloque.Find(id);
            db.Bloque.Remove(bloque);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
