﻿using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class ContactUsAPIController : ApiController
    {
        public IContactUsService _IContactUsService;
        public ContactUsAPIController()
        {
            _IContactUsService = new ContactUsService();
        }

        [HttpPost]
        public HttpResponseMessage SendContactUsDetails(StoreEntity storeEntity, ContactUsEntity contactUsEntity)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}