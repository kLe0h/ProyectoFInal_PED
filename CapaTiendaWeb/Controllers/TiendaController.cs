using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}