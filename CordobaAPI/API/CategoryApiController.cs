using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class CategoryApiController : ApiController
    {
        public ICategoryServices _categoryServices;
        public CategoryApiController()
        {
            _categoryServices = new CategoryService();
        }
        [HttpGet]
        public HttpResponseMessage GetCategoryList(int StoreId, int LoggedInUserId, int Category_Id = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryList(StoreId, LoggedInUserId, Category_Id);
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
        public HttpResponseMessage GetCategoryById(int Category_Id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _categoryServices.GetCategoryById(Category_Id, StoreId, LoggedInUserId);
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



        //Popular Category
        [HttpGet]
        public HttpResponseMessage GetCategoryListByStoreIdPopular( int LoggedInUserId, int storeID = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryListByStoreIdPopular(LoggedInUserId, storeID);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetStoreNameList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _categoryServices.GetStoreNameList(StoreId, LoggedInUserId);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCategoryAsPopular(int LoggedInUserId, CategoryPopularEntity CategoryPopularEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategoryAsPopular( LoggedInUserId, CategoryPopularEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpGet]
        public HttpResponseMessage GetLanguageList(int StoreId, int LoggedInUserId)
        {
            try
            {                
                var result = _categoryServices.GetLanguageList(StoreId, LoggedInUserId);
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
        public HttpResponseMessage GetParentCategoryList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _categoryServices.GetParentCategoryList(StoreId, LoggedInUserId);
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


        //Insert Or Update Category
        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCategory(int StoreId, int LoggedInUserId, CategoryEntity categoryEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategory(StoreId, LoggedInUserId, categoryEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Delete Category
        [HttpPost]
        public HttpResponseMessage DeleteCategory(int Category_Id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _categoryServices.DeleteCategory(Category_Id, StoreId, LoggedInUserId);
                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}