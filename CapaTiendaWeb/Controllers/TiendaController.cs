using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace CapaTiendaWeb.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        //redirigir usuario a esta vista que seria ya la tienda
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            ListaCategorias = new CN_Categoria().Listar();

            return Json(new { date = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}