using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiStackNet.Demo.Controllers
{
    public class ReadmeController : Controller
    {
        public ReadmeController()
        {

        }
        public ActionResult Index()
        {
            ViewBag.Title = "Readme File";

            return View();
        }
    }
}
