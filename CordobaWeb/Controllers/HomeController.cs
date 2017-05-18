
using CordobaCommon;
using CordobaCommon.GeneralMethods;
using CordobaModels;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;


namespace CordobaWeb.Controllers
{
   
    public class HomeController : Controller
    {        
        
        public static string LayoutNames = null;
        public ActionResult Index()
        {
            var masterView = View();
            int PortNumber = Request.Url.Port;
   
            try
            {
                  

                switch (PortNumber)
                {
                    case 8084:
                        LayoutNames = "Admin";
                        masterView.MasterName = string.Format("~/Views/Admin/{0}.cshtml", "_Layout");
                        break;
                    case 8085:
                        LayoutNames = "_Layout1";
                        masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml", "_Layout1");
                        break;
                    case 8086:
                        LayoutNames = "_Layout2";
                        masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml", "_Layout2");
                        break;

                }

                var Result = GetStoreDetailByUrl(Request.Url.AbsoluteUri);
                Result.template = LayoutNames; // This is Temporary
                ProjectSession.StoreSession = Result;                
                Session.Add("CssOverride",Result.css_overrides);
                return masterView;
          
            }
            catch (Exception)
            {
                throw;
            }
        
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


        public JsonResult GetStoreDetail(string URL)
        {            
            try
            {
                StoreDetail store = new StoreDetail();
                store = ProjectSession.StoreSession;
                return Json(store, JsonRequestBehavior.AllowGet); 
            }
            catch (Exception)
            {
                
                throw;
            }
        

        }


        public StoreDetail GetStoreDetailByUrl(String URL)
        {
            GenericRepository<StoreEntity> StoreRepository = new GenericRepository<StoreEntity>();          
            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("URL", URL) };
            var Result = StoreRepository.ExecuteSQL<StoreDetail>("GetStoreDetailByUrl", sqlParameter).FirstOrDefault();
            return Result;
        }

     
      

        
    }
}