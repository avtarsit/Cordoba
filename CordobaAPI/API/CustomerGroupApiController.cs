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
    public class CustomerGroupApiController : ApiController
    {
        public ICustomerGroupService _CustomerGroupService;

        public CustomerGroupApiController()
        {
            _CustomerGroupService = new CustomerGroupService();
        }

        [HttpGet]
        public HttpResponseMessage GetCustomerGroupList()
        {
            try
            {
                var result = _CustomerGroupService.GetCustomerGroupList();
                if(result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch(Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCustomerGroupDetail(int CustomerGroupId = 0)
        {
            try
            {
                var result = _CustomerGroupService.GetCustomerGroupDetail(CustomerGroupId);
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
        public HttpResponseMessage CreateOrUpdateCustomerGroup(CustomerGroupEntity CustomerGroupEntity)
        {
            try
            {
                var result = _CustomerGroupService.CreateOrUpdateCustomerGroup(CustomerGroupEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage DeleteCustomerGroup(int CustomerGroupId = 0)
        {
            try
            {
                var result = _CustomerGroupService.DeleteCustomerGroup(CustomerGroupId);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}