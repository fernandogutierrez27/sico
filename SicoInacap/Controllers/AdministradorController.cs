using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SicoInacap.Models;

namespace SicoInacap.Controllers
{
    public class AdministradorController : Controller
    {
        private SicoModel db = new SicoModel();

        // GET: Administrador
        public ActionResult Index()
        {
            return View();
        }

        //Busca el administrador
        public Administrador GetAdministrador(Usuario usuario)
        {
            bool encontrado = false;
            Administrador administrador = new Administrador();

            foreach(Administrador admin in db.Administrador.ToList())
            { 
                if (admin.Username.ToUpper().Equals(usuario.Username.ToUpper()))
                {
                    administrador = admin;
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
            {
                return administrador;
            }
            else
            {
                return null;

            }
            
        }
    }
}