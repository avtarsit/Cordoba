using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CordobaModels.Entities;

namespace CordobaAPI.API
{ 
    public class ProductPurchasedReportApiController : ApiController
    {

        public IProductPurchasedReportServices _productPurchasedReportService;
        public ProductPurchasedReportApiController()
        {
            _productPurchasedReportService = new ProductPurchasedReportService();
        }
        [HttpGet]
        public HttpResponseMessage GetOrderStatus(int language_id)
        {
            try
            {
                var result = _productPurchasedReportService.GetOrderStatus(language_id);
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