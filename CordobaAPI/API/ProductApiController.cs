﻿using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CordobaAPI.API
{
    public class ProductApiController : ApiController
    {
        public IProductServices _ProductServices;
        public ProductApiController()
        {
            _ProductServices = new ProductService();
        }
        [HttpGet]
        public HttpResponseMessage GetProductList()
        {
            try
            {
                var result = _ProductServices.GetProductList();
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
        public HttpResponseMessage GetProductById(int ProductId)
        {
            try
            {
                var result = _ProductServices.GetProductById(ProductId);
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