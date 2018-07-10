using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SicoInacap.Models;

namespace SicoInacap.Controllers
{

    public class SimpatizanteController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Simpatizante
        public ActionResult Index()
        {
            return View();
        }

        //Busca el simpatizante
        public Simpatizante GetSimpatizante(Usuario usuario)
        {
            bool encontrado = false;
            Simpatizante simpatizante = new Simpatizante();

            foreach (Simpatizante simp in db.Simpatizante.ToList())
            {
                if (simp.Username.ToUpper().Equals(usuario.Username.ToUpper()))
                {
                    simpatizante = simp;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                return simpatizante;
            }
            else
            {
                return null;

            }
        }
    }
}