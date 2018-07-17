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
            var recintos = db.Recinto.ToList();
            var agenda = db.Agenda.ToList();
            return View(recintos);
           
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