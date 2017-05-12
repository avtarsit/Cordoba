using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class ManufacturersApiController : ApiController
    {
        public IManufacturerServices _ManufacturerServices;
        // GET: api/ManufacturersApi
        public ManufacturersApiController()
        {
            _ManufacturerServices = new ManufacturerServices();
        }

        [HttpGet]
        public HttpResponseMessage GetManufacturersList()
        {
            try
            {
                var result = _ManufacturerServices.GetManufacturersList();
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
        public HttpResponseMessage GetManufaturerDetail(int manufacturer_id)
        {
            try
            {
                var result = _ManufacturerServices.GetManufaturerDetail(manufacturer_id);
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
        public HttpResponseMessage InsertUpdateManufacture(ManufacturersEntity manufacturersEntity)  
        {
            try
            {
                var result = _ManufacturerServices.InsertUpdateManufacture(manufacturersEntity);
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

        

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ManufacturersApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ManufacturersApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ManufacturersApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ManufacturersApi/5
        public void Delete(int id)
        {
        }
    }
}
