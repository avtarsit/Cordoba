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
    public class OrderApiController : ApiController
    {
        public IOrderService _orderService;
        public OrderApiController()
        {
            _orderService = new OrderService();
        }

        [HttpGet]
        public HttpResponseMessage GetOrderDetails(int orderId)
        {
            try
            {
                var result = _orderService.GetOrderDetails(orderId);
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
        public HttpResponseMessage InsertOrderHistory(OrderHistoryEntity objHistoryEntity)
        {
            try
            {
                var result = _orderService.InsertOrderHistory(objHistoryEntity);
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
