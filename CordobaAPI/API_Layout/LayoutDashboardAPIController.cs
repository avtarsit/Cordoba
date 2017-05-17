using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces_Layout;
using CordobaServices.Services_Layout;

namespace CordobaAPI.API_Layout
{
    public class LayoutDashboardAPIController : ApiController
    {
        public ILayoutDashboardServices _LayoutDashboardServices;

        public LayoutDashboardAPIController()
        {
            _LayoutDashboardServices = new LayoutDashboardServices();
        }

        [HttpGet]
        public HttpResponseMessage GetCategoryListByStoreId(int? StoreID,bool NeedToGetAllSubcategory)
        {
            try
            {
                var result = _LayoutDashboardServices.GetCategoryListByStoreId(StoreID, NeedToGetAllSubcategory);
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
