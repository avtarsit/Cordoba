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
    public class BannerApiController : ApiController
    {
        public IBannerServices _BannerServices;
        public BannerApiController()
        {
            _BannerServices = new BannerService();
        }
        [HttpGet]
        public HttpResponseMessage GetBannerList()
        {
            try
            {
                var result = _BannerServices.GetBannerList();
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
        public HttpResponseMessage GetBannerById(int BannerId)
        {
            try
            {
                var result = _BannerServices.GetBannerById(BannerId);
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