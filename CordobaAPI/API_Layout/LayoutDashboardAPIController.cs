using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces_Layout;
using CordobaServices.Services_Layout;
using System.Web.Http.Hosting;
using CordobaModels.Entities;

namespace CordobaAPI.API_Layout
{
    public class LayoutDashboardAPIController : ApiController
    {
        public ILayoutDashboardServices _LayoutDashboardServices;     

        public LayoutDashboardAPIController()
        {
            _LayoutDashboardServices = new LayoutDashboardServices();
        }

        [HttpGet]
        public HttpResponseMessage GetCategoryListByStoreId(int? StoreID,bool NeedToGetAllSubcategory)
        {
            try
            {
                var result = _LayoutDashboardServices.GetCategoryListByStoreId(StoreID, NeedToGetAllSubcategory);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetStoreDetailByUrl(String URL)
        {        
            try
            {
                var result = _LayoutDashboardServices.GetStoreDetailByUrl(URL);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }
         [HttpGet]
        public HttpResponseMessage GetLatestProductByStoreId(int StoreID)
        {        
            try
            {
                var result = _LayoutDashboardServices.GetLatestProductByStoreId(StoreID);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

         [HttpGet]
         public HttpResponseMessage GetPopularCategoryListByStoreId(int StoreID)
         {
             try
             {
                 var result = _LayoutDashboardServices.GetPopularCategoryListByStoreId(StoreID);
                 if (result != null)
                 {
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
             }
             catch (Exception)
             {

                 throw;
             }

         }

         [HttpPost]
         public HttpResponseMessage CustomerLogin(CustomerEntity CustomerObj)
         {
             try
             {
                 var result = _LayoutDashboardServices.CustomerLogin(CustomerObj);
                 if (result != null)
                 {
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
             }
             catch (Exception)
             {

                 throw;
             }

         }


         [HttpPost]
         public HttpResponseMessage AddtoWishList(wishlistEntity WishObj)
         {
             try
             {
                 var result = _LayoutDashboardServices.AddtoWishList(WishObj);
                 if (result != null)
                 {
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
             }
             catch (Exception)
             {

                 throw;
             }

         }

        [HttpGet]
         public HttpResponseMessage RemoveFromWishList(int StoreID, int product_id, int Customer_Id)
         {
             try
             {
                 var result = _LayoutDashboardServices.RemoveFromWishList(StoreID, product_id, Customer_Id);
                 if (result != null)
                 {
                     return Request.CreateResponse(HttpStatusCode.OK, result);
                 }
                 return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
             }
             catch (Exception)
             {

                 throw;
             }

         }
        

        

    }
}
