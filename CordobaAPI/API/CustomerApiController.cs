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
    public class CustomerApiController : ApiController
    {
        public ICustomerService _CustomerService;

        public CustomerApiController()
        {
            _CustomerService = new CustomerService();
        }

        [HttpGet]
        public HttpResponseMessage GetCustomerList()
        {
            try
            {
                var result = _CustomerService.GetCustomerList();
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
