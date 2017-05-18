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
    public class CurrencyApiController : ApiController
    {
        public  ICurrencyService _CurrencyService;
        // GET: CurrencyApi
        public CurrencyApiController()
        {
            _CurrencyService = new CurrencyService();
        }

        [HttpGet]
        public HttpResponseMessage GetCurrencyList()
        {
            try
            {
                var result = _CurrencyService.GetCurrencyList();
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
        public HttpResponseMessage GetCurrencyDetail(int CurrencyId = 0)
        {
            try
            {
                var result = _CurrencyService.GetCurrencyDetail(CurrencyId);
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
        public HttpResponseMessage CreateOrUpdateCurrency(CurrencyEntity CurrencyEntity)
        {
            try
            {
                var result = _CurrencyService.CreateOrUpdateCurrency(CurrencyEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage DeleteCurrency(int CurrencyId = 0)
        {
            try
            {
                var result = _CurrencyService.DeleteCurrency(CurrencyId);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}