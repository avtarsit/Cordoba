﻿using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class DashboardApiController : ApiController
    {
        public IDashboardService _dashboardService;
        public DashboardApiController()
        {
            _dashboardService = new DashboardService();
        }

        [HttpGet]
        public HttpResponseMessage GetLatestOrderDetailsDashboard(int storeId)
        {
            try
            {
                var result = _dashboardService.GetLatestOrderDetailsDashboard(storeId);
                if (result != null)
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
    }
}
