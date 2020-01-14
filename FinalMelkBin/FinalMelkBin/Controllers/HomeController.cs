using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalMelkBin.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace FinalMelkBin.Controllers
{
    public class HomeController : Controller
    {
        //[Authorize(Roles = "SuperAdmin")]

        public ActionResult Index()
        {
           
            return View();
        }

    }
}