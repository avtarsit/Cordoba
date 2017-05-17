﻿using CordobaModels.Entities;
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
    public class CountryApiController : ApiController
    {
        public ICountryServices _countryServices;
        public CountryApiController()
        {
            _countryServices = new CountryServices();
        }


        [HttpGet]
        public HttpResponseMessage GetCountryList(int countryId)
        {
            try
            {
                var result = _countryServices.GetCountryList(countryId);
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
        public HttpResponseMessage InsertOrUpdateCountry(CountryEntity objCountry)
        {
            int result = _countryServices.InsertOrUpdateCountry(objCountry);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        public HttpResponseMessage DeleteCountry(int countryId)
        {
            try
            {
                var result = _countryServices.DeleteCountry(countryId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }


        // GET: api/CountryApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CountryApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CountryApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CountryApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CountryApi/5
        public void Delete(int id)
        {
        }
    }
}
