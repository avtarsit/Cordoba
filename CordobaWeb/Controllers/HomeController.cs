using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CordobaWeb.Controllers
{
    public class HomeController : Controller
    {
        public static string LayoutNames = null;
        public ActionResult Index()
        {
            var masterView = View();
            int PortNumber = Request.Url.Port;
        

            switch (PortNumber)
            {
                case  8084:
                            LayoutNames = "Admin";
                            masterView.MasterName = string.Format("~/Views/Admin/{0}.cshtml", "_Layout");
                            break;
                case 8085 :
                            LayoutNames = "_Layout1";
                            masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml","_Layout1");
                            break;
                case  8086:
                            LayoutNames = "_Layout2";
                            masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml","_Layout2");
                            break;

            }
           
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
            string LayoutName=null;
            int PortNumber = Request.Url.Port;
            switch (PortNumber)
            {
                case 8084:
                     LayoutName= "Admin";                   
                           break;
                case 8085:
                     LayoutName = "_Layout1";           
                    break;
                case 8086:
                    LayoutName = "_Layout2";
                    break;

            }
            return LayoutName;

        }
    }
}