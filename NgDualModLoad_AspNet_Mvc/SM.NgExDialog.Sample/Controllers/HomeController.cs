using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SM.NgExDialog.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.IsAngularDebugLoader = Convert.ToBoolean(ConfigurationManager.AppSettings["IsAngularDebugLoader"] ?? "false");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
       
    }
}