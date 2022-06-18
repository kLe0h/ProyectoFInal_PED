using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

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

        public ActionResult Restablecer(){
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

                ViewBag.Error = null;

                return RedirectToAction("Index","Home");
            }
        }

    }
}