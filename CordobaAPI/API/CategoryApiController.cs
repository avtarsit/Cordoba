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
        public HttpResponseMessage GetCategoryList(int CategoryId = 0)
        {
            try
            {
                var result = _categoryServices.GetCategoryList(CategoryId);
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