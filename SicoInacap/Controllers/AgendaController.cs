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

        // GET: Agenda/Calendario
        public ActionResult Calendario()
        {

            try
            {
                string recintoId = Request.QueryString["recintoId"];
                ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Nombre", int.Parse(recintoId));
            }
            catch (Exception ex)
            {
                ViewBag.CodigoRecinto = new SelectList(db.Recinto, "Codigo", "Nombre");
            }

            ViewBag.CodigoEvento = new SelectList(db.Evento.Where(e => e.EstadoEvento.Codigo == 1).ToList(), "Codigo", "Descripcion");
            ViewBag.NuevoCodigoRecinto = new SelectList(db.Recinto, "Codigo", "Nombre");
            ViewBag.HoraInicio = new SelectList(db.Bloque, "HoraInicio", "HoraInicio");
            return View();
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
                        id = item.Evento.Codigo,
                        title = item.Evento.Nombre,
                        description = item.Evento.Descripcion,
                        start = item.HoraInicio.Year + "-" + item.HoraInicio.Month + "-" + item.HoraInicio.Day + " " + item.HoraInicio.Hour + ":00:00",
                        end = item.HoraTermino.Year + "-" + item.HoraTermino.Month + "-" + item.HoraTermino.Day + " " + item.HoraTermino.Hour + ":00:00",
                        color = "#01579b"
                    });
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private void UpdateAgendaBloques(Agenda agenda)
        {
            DateTime fechaInicio = new DateTime(agenda.HoraInicio.Year, agenda.HoraInicio.Month, agenda.HoraInicio.Day);
            DateTime fechaFin = new DateTime(agenda.HoraTermino.Year, agenda.HoraTermino.Month, agenda.HoraTermino.Day);
            for (DateTime fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
            {
                int horaInicio = fecha.Day.Equals(agenda.HoraInicio.Day) && fecha.Month.Equals(agenda.HoraInicio.Month) && fecha.Year.Equals(agenda.HoraInicio.Year) ? agenda.HoraInicio.Hour : 0;
                int horaFin = fecha.Day.Equals(agenda.HoraTermino.Day) && fecha.Month.Equals(agenda.HoraTermino.Month) && fecha.Year.Equals(agenda.HoraTermino.Year) ? agenda.HoraTermino.Hour : 0;
                if (horaFin.Equals(0) && fecha.Equals(fechaFin))
                    break;
                this.GetRange(horaInicio, horaFin).ForEach(bloque => db.AgendaBloque.Add(new AgendaBloque { Agenda = agenda, CodigoAgenda = (int)agenda.Codigo, Bloque = bloque, CodigoBloque = bloque.Codigo, Fecha = fecha }));
            }
            db.SaveChanges();
        }

        private List<Bloque> GetRange(int start, int end)
        {
            return start == 0 && end == 0 ? db.Bloque.ToList() : db.Bloque.Where(b => b.HoraInicio.Hours >= start && (b.HoraInicio.Hours < end || end == 0)).ToList();
        }

        public JsonResult SaveAgenda([Bind(Include = "CodigoEvento,CodigoRecinto,HoraInicio,HoraTermino")] Agenda agenda)
        {
            try
            {
                List<Agenda> list = db.Agenda.Where(a => a.Evento.Codigo.Equals(agenda.CodigoEvento)).ToList();
                if (list.Count > 0 && list.First().Evento.CodigoEstado == 2)
                {
                    Agenda original = db.Agenda.Find(list.First().Codigo);
                    original.Recinto = db.Recinto.Find(agenda.CodigoRecinto);
                    original.CodigoRecinto = agenda.CodigoRecinto;
                    original.HoraInicio = agenda.HoraInicio;
                    original.HoraTermino = agenda.HoraTermino;
                    if (db.SaveChanges() > 0)
                    {
                        List<AgendaBloque> lst = new List<AgendaBloque>();
                        foreach (AgendaBloque item in db.AgendaBloque.ToList())
                            if (item.CodigoAgenda.Equals(original.Codigo))
                                lst.Add(item);
                        lst.ForEach(ab => db.AgendaBloque.Remove(ab));
                        this.UpdateAgendaBloques(original);
                    }
                }
                else
                {
                    agenda.Evento = db.Evento.Find(agenda.CodigoEvento);
                    agenda.Recinto = db.Recinto.Find(agenda.CodigoRecinto);
                    db.Agenda.Add(agenda);

                    if (db.SaveChanges() > 0)
                    {
                        db.Evento.Find(agenda.Evento.Codigo).EstadoEvento = db.EstadoEvento.Find(2);
                        if (db.SaveChanges() > 0)
                            this.UpdateAgendaBloques(agenda);
                    }
                }

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
