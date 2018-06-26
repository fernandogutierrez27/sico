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
    public class EstadoEventoController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: EstadoEvento
        public ActionResult Index()
        {
            return View(db.EstadoEvento.ToList());
        }

        // GET: EstadoEvento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEvento estadoEvento = db.EstadoEvento.Find(id);
            if (estadoEvento == null)
            {
                return HttpNotFound();
            }
            return View(estadoEvento);
        }

        // GET: EstadoEvento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoEvento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] EstadoEvento estadoEvento)
        {
            if (ModelState.IsValid)
            {
                db.EstadoEvento.Add(estadoEvento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoEvento);
        }

        // GET: EstadoEvento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEvento estadoEvento = db.EstadoEvento.Find(id);
            if (estadoEvento == null)
            {
                return HttpNotFound();
            }
            return View(estadoEvento);
        }

        // POST: EstadoEvento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] EstadoEvento estadoEvento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoEvento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoEvento);
        }

        // GET: EstadoEvento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoEvento estadoEvento = db.EstadoEvento.Find(id);
            if (estadoEvento == null)
            {
                return HttpNotFound();
            }
            return View(estadoEvento);
        }

        // POST: EstadoEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoEvento estadoEvento = db.EstadoEvento.Find(id);
            db.EstadoEvento.Remove(estadoEvento);
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
