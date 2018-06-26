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

    public class AgendaController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Agenda
        public ActionResult Index()
        {
            var agenda = db.Agenda.Include(a => a.Evento).Include(a => a.Recinto);
            return View(agenda.ToList());
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            ViewBag.CodigoEvento = new SelectList(db.Evento, "Codigo", "Descripcion");
            ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Nombre");
            ViewBag.HoraInicio = new SelectList(db.Bloque, "HoraInicio", "HoraInicio");
            return View();
        }

        public JsonResult GetAgenda()
        {
            string recintoId = Request.QueryString["recintoId"];
            List<Object> list = new List<object>();
            foreach (Agenda item in db.Agenda.ToList<Agenda>())
                if (item.CodigoRecinto.ToString().Equals(recintoId))
                    list.Add(new
                    {
                        title = item.Evento.Nombre,
                        description = item.Evento.Descripcion,
                        start = item.HoraInicio.Year + "-" + item.HoraInicio.Month + "-" + item.HoraInicio.Day + " " + item.HoraInicio.Hour + ":00:00",
                        end = item.HoraTermino.Year + "-" + item.HoraTermino.Month + "-" + item.HoraTermino.Day + " " + item.HoraTermino.Hour + ":00:00",
                        color = "#01579b"
                    });
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveAgenda([Bind(Include = "CodigoEvento,CodigoRecinto,HoraInicio,HoraTermino")] Agenda agenda)
        {
            try
            {
                agenda.Evento = db.Evento.Find(agenda.CodigoEvento);
                agenda.Recinto = db.Recinto.Find(agenda.CodigoRecinto);
                db.Agenda.Add(agenda);
                db.SaveChanges();
                return new JsonResult { Data = agenda.CodigoRecinto, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch(Exception ex)
            {
                return new JsonResult { Data = ex.Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // POST: Agenda/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,CodigoEvento,CodigoRecinto,HoraInicio,HoraTermino")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                db.Agenda.Add(agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoEvento = new SelectList(db.Evento, "Codigo", "Descripcion", agenda.CodigoEvento);
            ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Latitud", agenda.CodigoRecinto);
            return View(agenda);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoEvento = new SelectList(db.Evento, "Codigo", "Descripcion", agenda.CodigoEvento);
            ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Latitud", agenda.CodigoRecinto);
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,CodigoEvento,CodigoRecinto,HoraInicio,HoraTermino")] Agenda agenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoEvento = new SelectList(db.Evento, "Codigo", "Descripcion", agenda.CodigoEvento);
            ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Latitud", agenda.CodigoRecinto);
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agenda agenda = db.Agenda.Find(id);
            db.Agenda.Remove(agenda);
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
