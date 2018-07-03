using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SicoInacap.Models;

namespace SicoInacap.Controllers
{
    public class LoginController : Controller
    {

        private SicoModel db = new SicoModel();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        //Realiza el Login
        [HttpPost]
        public ActionResult Authorize(SicoInacap.Models.Usuario usuarioLogin)
        {

            Usuario usuario = db.Usuario.Where(u => u.Username.ToUpper() == usuarioLogin.Username.ToUpper() && u.Password.ToUpper() == usuarioLogin.Password.ToUpper()).FirstOrDefault();

            if (usuario == null)
            {
                ViewData["error"] = "Username o Password incorrecto.";
                //return View("index", usuarioLogin);
                return View("index");
            }
            else
            {

                SimpatizanteController simpController = new SimpatizanteController();

                usuario.Simpatizante = simpController.GetSimpatizante(usuario);

                AdministradorController adminController = new AdministradorController();

                usuario.Administrador = adminController.GetAdministrador(usuario);


                if (usuario.Simpatizante == null || usuario.Administrador == null)
                {
                    ViewData["error"] = "Usted debe ser Simpatizante y Administrador para ingresar.";
                    return View("index");
                }
                else
                {
                        Session["nombreUsuario"] = usuario.Simpatizante.Nombres + " " + usuario.Simpatizante.Apellidos;
                        Session["username"] = usuario.Username;
                    
                    return RedirectToAction("index", "Home");
                }
                
            }
        }

        //Cierra Sesión
        public ActionResult Logout()
        {
                Session.Abandon();
                return RedirectToAction("index", "Login");
        }
    }
}