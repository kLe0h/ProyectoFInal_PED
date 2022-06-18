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

        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.IdUsuario == 0)
            {
                //al ser un usuario nuevo
                resultado = new CN_usuarios().Registrar(objeto, out mensaje); //devolver ID usuario

            } else
            {
                resultado = new CN_usuarios().Editar(objeto, out mensaje);
            }
            

            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        //url que devuelve datos sin necesitar valores
        [HttpGet]
        public JsonResult ListaReporte(string fechaInicio, string fechFin, string idTransaccion){
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reporte().Venta(fechaInicio, fechFin, idTransaccion);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //url que devuelve datos sin necesitar valores
        [HttpGet]
        public JsonResult VistaDashBoard(){
            DashBoard objeto = new CN_Reporte().VerDashboard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

    }
}