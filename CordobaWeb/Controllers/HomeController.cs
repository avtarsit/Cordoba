
using CordobaCommon;
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
      
        public ActionResult Index()
        {
            var masterView = View();           
            try
            {
                var Result = GetStoreDetailByUrl(Request.Url.Authority);
                switch (Result.template.ToLower())
                {
                    case "admin":
                        if (ProjectSession.AdminLoginSession == null)
                        {
                            return RedirectToAction("Login", "Admin");
                        }                      
                        masterView.MasterName = string.Format("~/Views/Admin/{0}.cshtml", "_Layout");
                        break;
                    case "_layout1":
                    case "_layout2":
                        if (Request.Url.Port == 1021)
                        {
                            masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml", "_Layout1");
                        }
                        else
                        {
                            masterView.MasterName = string.Format("~/Views/Layouts/{0}.cshtml", "_Layout2");
                        }
                        break;                                         
                }
            
                ProjectSession.StoreSession = Result;
                Session.Add("CssOverride", Result.css_overrides);

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

        public ActionResult accessdenied()
        {
            return View();
        }

        public JsonResult GetAdminUserDetail()
        {
            UserEntity userDetail = new UserEntity();
            userDetail = ProjectSession.AdminLoginSession;
            return Json(userDetail, JsonRequestBehavior.AllowGet);
        }
    }
}