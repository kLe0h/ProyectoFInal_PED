using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Web.Security;
using CapaEntidad;
using CapaNegocio;

namespace CapaAdministador.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Index(){
            return View();
        }

        public ActionResult CambiarClave(){
            return View();
        }

        public ActionResult ReestablecerClave(){
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave){

            Usuario oUsuario = new Usuario();

            oUsuario = new CN_usuarios().Listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSHA256(clave)).FirstOrDefault();

            if(oUsuario == null){
                ViewBag.Error = "Correo o contraseña incorrecto";
                return View();
            }else{

                if (oUsuario.Reestablecer){
                    TempData["IdUsuario"] = oUsuario.IdUsuario;
                    return RedirectToAction("CambiarClave");
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false);

                ViewBag.Error = null;

                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idUsuario, string claveActual, string nuevaClave, string confirmarClave){

            Usuario oUsuario = new Usuario();
            oUsuario = new CN_usuarios().Listar().Where(u => u.IdUsuario == int.Parse(idUsuario)).FirstOrDefault();

            if(oUsuario.Clave != CN_Recursos.ConvertirSHA256(claveActual)){

                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = "";
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if(nuevaClave != confirmarClave){

                TempData["IdUsuario"] = idUsuario;
                ViewData["vclave"] = claveActual;
                ViewBag.Error = "La contraseña no coincide";
                return View();
            }

            ViewData["vclave"] = "";
            nuevaClave = CN_Recursos.ConvertirSHA256(nuevaClave);
            
            string mensaje = string.Empty;

            bool respuesta = new CN_usuarios().CambiarClave(int.Parse(idUsuario), nuevaClave, out mensaje);
            if (respuesta){
                return RedirectToAction("Index");
            }else{
                TempData["IdUsuario"] = idUsuario;
                ViewBag.Error = mensaje;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ReestablecerClave(string correo)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_usuarios().Listar().Where(item => item.Correo == correo).FirstOrDefault();
            
            if(oUsuario == null) {
                ViewBag.Error = "No se encontro un usuario relacionado a ese correo";

            }

            string mensaje = string.Empty;

            bool respuesta = new CN_usuarios().ReestablecerClave(oUsuario.IdUsuario, correo, out mensaje);
            if (respuesta){
                return RedirectToAction("Index", "Acceso");
            }
            else{
                ViewBag.Error = mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion(){
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }
    }
}