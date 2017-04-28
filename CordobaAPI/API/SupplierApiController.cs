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
    public class SupplierApiController : ApiController
    {
        public ISupplierServices _SupplierServices;

        public SupplierApiController()
        {
            _SupplierServices =new SupplierServices();
        }



        [HttpGet]
        public HttpResponseMessage GetSupplierList(int? SupplierID)
        {
            try
            {
                var result = _SupplierServices.GetSupplierList(SupplierID);
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
        public HttpResponseMessage GetSupplierDetail(int? SupplierID)
        {
            try
            {
                var result = _SupplierServices.GetSupplierDetail(SupplierID);
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

        // GET: api/SupplierApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SupplierApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SupplierApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SupplierApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SupplierApi/5
        public void Delete(int id)
        {
        }
    }
}
