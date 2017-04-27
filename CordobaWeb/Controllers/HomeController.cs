using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CordobaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var masterView = View();
            masterView.MasterName = string.Format("~/Views/Admin/{0}.cshtml", "_Layout");
            return masterView;
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public string GetLayoutName(string HostName)
        {
           
            return "Admin";
        }
    }
}