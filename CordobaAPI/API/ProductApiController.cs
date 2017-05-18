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
        public HttpResponseMessage GetProductById(int product_id)
        {
            try
            {
                var result = _ProductServices.GetProductById(product_id);
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
        public HttpResponseMessage AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id)
        {
            try
            {
                var result = _ProductServices.AddProductToCart(  store_id,  customer_id,  product_id,  qty,  cartgroup_id);
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
        public HttpResponseMessage DeleteProductFromCart(int cart_id)
        {
            try
            {
                var result = _ProductServices.DeleteProductFromCart(cart_id);
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