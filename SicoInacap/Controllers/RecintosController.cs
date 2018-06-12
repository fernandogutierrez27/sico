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
    public class RecintosController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Recintoes
        public ActionResult Index()
        {
            return View(db.Recinto.ToList());
        }

        // GET: Recintoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recinto recinto = db.Recinto.Find(id);
            if (recinto == null)
            {
                return HttpNotFound();
            }
            return View(recinto);
        }

        // GET: Recintoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recintoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Latitud,Longitud,Nombre")] Recinto recinto)
        {
            if (ModelState.IsValid)
            {
                db.Recinto.Add(recinto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recinto);
        }

        // GET: Recintoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recinto recinto = db.Recinto.Find(id);
            if (recinto == null)
            {
                return HttpNotFound();
            }
            return View(recinto);
        }

        // POST: Recintoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Latitud,Longitud,Nombre")] Recinto recinto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recinto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recinto);
        }

        // GET: Recintoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recinto recinto = db.Recinto.Find(id);
            if (recinto == null)
            {
                return HttpNotFound();
            }
            return View(recinto);
        }

        // POST: Recintoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recinto recinto = db.Recinto.Find(id);
            db.Recinto.Remove(recinto);
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
