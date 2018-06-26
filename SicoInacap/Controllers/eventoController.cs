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
    public class eventoController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: evento
        public ActionResult Index()
        {
            var evento = db.Evento.Include(e => e.Administrador).Include(e => e.Categoria).Include(e => e.EstadoEvento).Include(e => e.Miembro);
            return View(evento.ToList());
        }

        // GET: evento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: evento/Create
        public ActionResult Create()
        {
            ViewBag.UsernameOrganizador = new SelectList(db.Administrador, "Username", "Username");
            ViewBag.CodigoCategoria = new SelectList(db.Categoria, "Codigo", "Nombre");
            ViewBag.CodigoEstado = new SelectList(db.EstadoEvento, "Codigo", "Nombre");
            ViewBag.UsernameResponsable = new SelectList(db.Miembro, "Username", "Fono");
            return View();
        }

        // POST: evento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,CodigoCategoria,CodigoEstado,Descripcion,Nombre,UsernameOrganizador,UsernameResponsable")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Evento.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsernameOrganizador = new SelectList(db.Administrador, "Username", "Username", evento.UsernameOrganizador);
            ViewBag.CodigoCategoria = new SelectList(db.Categoria, "Codigo", "Nombre", evento.CodigoCategoria);
            ViewBag.CodigoEstado = new SelectList(db.EstadoEvento, "Codigo", "Nombre", evento.CodigoEstado);
            ViewBag.UsernameResponsable = new SelectList(db.Miembro, "Username", "Fono", evento.UsernameResponsable);
            return View(evento);
        }

        // GET: evento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsernameOrganizador = new SelectList(db.Administrador, "Username", "Username", evento.UsernameOrganizador);
            ViewBag.CodigoCategoria = new SelectList(db.Categoria, "Codigo", "Nombre", evento.CodigoCategoria);
            ViewBag.CodigoEstado = new SelectList(db.EstadoEvento, "Codigo", "Nombre", evento.CodigoEstado);
            ViewBag.UsernameResponsable = new SelectList(db.Simpatizante.Where(simpatizante => db.Miembro.Any(miembro => miembro.Username == simpatizante.Username)), "Username", "Nombres", evento.UsernameResponsable);
            return View(evento);
        }

        // POST: evento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,CodigoCategoria,CodigoEstado,Descripcion,Nombre,UsernameOrganizador,UsernameResponsable")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsernameOrganizador = new SelectList(db.Administrador, "Username", "Username", evento.UsernameOrganizador);
            ViewBag.CodigoCategoria = new SelectList(db.Categoria, "Codigo", "Nombre", evento.CodigoCategoria);
            ViewBag.CodigoEstado = new SelectList(db.EstadoEvento, "Codigo", "Nombre", evento.CodigoEstado);
            ViewBag.UsernameResponsable = new SelectList(db.Miembro, "Username", "Fono", evento.UsernameResponsable);
            return View(evento);
        }

        // GET: evento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
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
