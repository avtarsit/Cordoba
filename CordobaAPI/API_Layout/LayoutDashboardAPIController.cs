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
        public HttpResponseMessage GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory)
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

        [HttpGet]
        public HttpResponseMessage GetHotDealsListByStoreId(int StoreID)
        {
            try
            {
                var result = _LayoutDashboardServices.GetHotDealsListByStoreId(StoreID);
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
        public HttpResponseMessage GetSpecialOfferListByStoreId(int StoreID)
        {
            try
            {
                var result = _LayoutDashboardServices.GetSpecialOfferListByStoreId(StoreID);
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
         public HttpResponseMessage GetBestSellerListByStoreId(int StoreID)
         {
             try
             {
                 var result = _LayoutDashboardServices.GetBestSellerListByStoreId(StoreID);
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

         [HttpGet]
         public HttpResponseMessage GetHotDealsListByStoreId(int StoreID)
         {
             try
             {
                 var result = _LayoutDashboardServices.GetHotDealsListByStoreId(StoreID);
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
         public HttpResponseMessage GetSpecialOfferListByStoreId(int StoreID)
         {
             try
             {
                 var result = _LayoutDashboardServices.GetSpecialOfferListByStoreId(StoreID);
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

        [HttpGet]
        public HttpResponseMessage CustomerDetailLayout(int CustomerId, int StoreId)
        {
            try
            {
                var result = _LayoutDashboardServices.CustomerDetailLayout(CustomerId, StoreId);
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
        public HttpResponseMessage SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.SaveCustomerBasicDetails_Layout(StoreId, CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public HttpResponseMessage SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.SaveChangedPassword_Layout(StoreId, CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetCustomerAddressList_Layout(int StoreId, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetCustomerAddressList_Layout(StoreId, customer_id);
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
        public HttpResponseMessage AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj)
        {
            try
            {
                var result = _LayoutDashboardServices.AddOrUpdateAddressDetail_Layout(StoreId, AddressObj);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet]
        public HttpResponseMessage DeleteCustomerAddress(int StoreId, int customer_id, int address_id)
        {
            try
            {
                var result = _LayoutDashboardServices.DeleteCustomerAddress(StoreId, customer_id, address_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage GetStoreImageList(int Store_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetStoreImageList(Store_Id);
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
