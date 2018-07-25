using SicoInacap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SicoInacap.Controllers
{
    public class HomeController : Controller
    {
        private SicoModel db = new SicoModel();

        public ActionResult Index()
        {
            ViewBag.Recintos = db.Recinto.ToList();
            ViewBag.Agendas = db.Agenda.ToList();
            ViewBag.Usuarios = db.Usuario.ToList();
            ViewBag.Eventos = db.Evento.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}