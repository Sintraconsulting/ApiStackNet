using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiStackNet.Demo.Controllers
{
    public class ReadmeFileController : Controller
    {
        public ReadmeFileController()
        {

        }
        public ActionResult Index()
        {
            ViewBag.Title = "Readme File";

            return View();
        }
    }
}
