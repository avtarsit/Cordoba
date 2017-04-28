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
    public class ProductCatalogueApiController : ApiController
    {
        
        public IProductCatalogueServices _productCatalogueServices;
        public ProductCatalogueApiController()
        {
            _productCatalogueServices = new ProductCatalogueService();
        }
        [HttpGet]
        public HttpResponseMessage GetProductCatalogueList(int ProductCatalogueId = 0)
        {
            try
            {
                var result = _productCatalogueServices.GetProductCatalogueList();
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
        public HttpResponseMessage GetProductCatalogueById(int ProductCatalogueId)
        {
            try
            {
                var result = _productCatalogueServices.GetProductCatalogueById(ProductCatalogueId);
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