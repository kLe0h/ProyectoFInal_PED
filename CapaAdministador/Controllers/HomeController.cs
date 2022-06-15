using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;


namespace CapaAdministador.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //Metodo que se manda a llamar en _Layout.cshtml
        public ActionResult Usuarios()
        {
            return View();
        }

        //url que devuelve datos sin necesitar valores
        [HttpGet] 
        public JsonResult ListUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();
            
            oLista = new CN_usuarios().Listar();

            return Json(new { data = oLista } , JsonRequestBehavior.AllowGet);
        }


    }
}