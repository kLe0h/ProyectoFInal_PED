﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


    }
}