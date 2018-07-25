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
    public class UsuariosController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Usuarios
        public ActionResult Index(bool? AdminPromovido = false, bool? NoEliminado = false, bool? Eliminado = false)
        {
            var usuario = db.Usuario.Include(u => u.Administrador).Include(u => u.Miembro).Include(u => u.Simpatizante);

            ViewBag.eliminado = Eliminado;
            ViewBag.promovido = AdminPromovido;
            ViewBag.noeliminado = NoEliminado;
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult PromoverAdmin(string usuarioId)
        {
            Administrador admin = db.Administrador.Find(usuarioId);
            if (admin != null) return RedirectToAction("Index");
            db.Administrador.Add(new Administrador
            {
                Username = usuarioId

            });
            db.SaveChanges();
            return RedirectToAction("Index", new { AdminPromovido = true });
        }

        public ActionResult RemoverAdmin(string usuarioId)
        {
            Administrador admin = db.Administrador.Find(usuarioId);
            if (admin == null) return RedirectToAction("Index");
            db.Administrador.Remove(admin);
            try
            {
                db.SaveChanges();
            }catch(Exception e)
            {

                return RedirectToAction("Index", new { NoEliminado = true });
            }

            return RedirectToAction("Index", new { Eliminado = true });
        }

        public ActionResult ConvertirMiembro(string usuarioId)
        {
            Miembro miembro = db.Miembro.Find(usuarioId);
            if (miembro != null) RedirectToAction("Index");
            miembro = new Miembro
            {
                Username = usuarioId,
                FechaIngreso = DateTime.Today
            };
            ViewBag.CodigoCargo = new SelectList(db.Cargo, "Codigo", "Nombre");

            return View(miembro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConvertirMiembro([Bind(Include = "Username,CodigoCargo,Run,FechaIngreso,Fono")] Miembro miembro)
        {
            if (ModelState.IsValid)
            {
                db.Miembro.Add(miembro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.Cargo, "Codigo", "Nombre");
            
            return View(miembro);
        }

        public ActionResult ConvertirSimpatizante(string usuarioId)
        {
            Miembro miembro = db.Miembro.Find(usuarioId);
            if (miembro == null) RedirectToAction("Index");
            try
            {
                db.Miembro.Remove(miembro);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Administrador, "Username", "Username");
            ViewBag.Username = new SelectList(db.Miembro, "Username", "Fono");
            ViewBag.Username = new SelectList(db.Simpatizante, "Username", "Apellidos");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,FechaInscripcion,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaInscripcion = DateTime.Today;
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Administrador, "Username", "Username", usuario.Username);
            ViewBag.Username = new SelectList(db.Miembro, "Username", "Fono", usuario.Username);
            ViewBag.Username = new SelectList(db.Simpatizante, "Username", "Apellidos", usuario.Username);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Administrador, "Username", "Username", usuario.Username);
            ViewBag.Username = new SelectList(db.Miembro, "Username", "Fono", usuario.Username);
            ViewBag.Username = new SelectList(db.Simpatizante, "Username", "Apellidos", usuario.Username);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,FechaInscripcion,Password")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Administrador, "Username", "Username", usuario.Username);
            ViewBag.Username = new SelectList(db.Miembro, "Username", "Fono", usuario.Username);
            ViewBag.Username = new SelectList(db.Simpatizante, "Username", "Apellidos", usuario.Username);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
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
