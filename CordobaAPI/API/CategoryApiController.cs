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
        public HttpResponseMessage GetCategoryList(int Category_Id = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryList(Category_Id);
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
        public HttpResponseMessage GetCategoryById(int Category_Id)
        {
            try
            {
                var result = _categoryServices.GetCategoryById(Category_Id);
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
        public HttpResponseMessage GetCategoryListByStoreIdPopular(int storeID = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryListByStoreIdPopular(storeID);
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
        public HttpResponseMessage GetStoreNameList()
        {
            try
            {
                var result = _categoryServices.GetStoreNameList();
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
        public HttpResponseMessage InsertOrUpdateCategoryAsPopular(CategoryPopularEntity CategoryPopularEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategoryAsPopular(CategoryPopularEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }



        [HttpGet]
        public HttpResponseMessage GetLanguageList()
        {
            try
            {                
                var result = _categoryServices.GetLanguageList();
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
        public HttpResponseMessage GetParentCategoryList()
        {
            try
            {
                var result = _categoryServices.GetParentCategoryList();
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
        public HttpResponseMessage InsertOrUpdateCategory(CategoryEntity categoryEntity)
        {
            try
            {
                var result = _categoryServices.InsertOrUpdateCategory(categoryEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Delete Category
        [HttpPost]
        public HttpResponseMessage DeleteCategory(int Category_Id)
        {
            try
            {
                var result = _categoryServices.DeleteCategory(Category_Id);
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