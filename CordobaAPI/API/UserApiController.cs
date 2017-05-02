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
    public class UserApiController : ApiController
    {

        public IUserServices _UserServices;

        public UserApiController()
        {
            _UserServices = new UserServices();
        }

        [HttpGet]
        public HttpResponseMessage GetUserList()
        {
            try
            {
                var result = _UserServices.GetUserList();
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
        public HttpResponseMessage GetUserDetail(int UserID = 0)
        {
            try
            {
                var result = _UserServices.GetUserDetail(UserID);
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
      

        // GET: api/UserApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UserApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserApi/5
        public void Delete(int id)
        {
        }
    }
}
