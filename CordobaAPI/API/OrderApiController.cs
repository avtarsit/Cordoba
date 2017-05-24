using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices;
using CordobaServices.Helpers;

namespace CordobaAPI.API
{
    public class OrderApiController : ApiController
    {
        public IOrderService _orderService;
        public ICountryServices _countryServices;
        public OrderApiController()
        {
            _orderService = new OrderService();
            _countryServices = new CountryServices();
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

        [HttpPost]
        public TableParameter<OrderEntity> GetOrderList(int PageIndex, TableParameter<OrderEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _orderService.GetOrderList(sortColumn, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<OrderEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCustomerGroupList()
        {
            var customerGroup = _orderService.GetCustomerGroupList();
            if (customerGroup != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerGroup);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        }

        [HttpGet]
        public HttpResponseMessage GetCurrencyList()
        {
            var currency = _orderService.GetCurrencyList();
            if (currency != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, currency);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        }


        [HttpGet]
        public HttpResponseMessage GetCountryList(int countryId)
        {
            try
            {
                var result = _countryServices.GetCountryList(countryId).OrderBy(m => m.name).ToList();
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
        public HttpResponseMessage GetZoneListByCountry(int countryId)
        {
            try
            {
                var result = _orderService.GetZoneListByCountry(countryId).ToList();
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

        public HttpResponseMessage GetCustomerAddress(int orderId)
        {
            try
            {
                var result = _orderService.GetCustomerAddress(orderId);
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
        public HttpResponseMessage UpdateOrder_CutomerDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_CutomerDetails(objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrder_PaymentDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_PaymentDetails(objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrder_ShippingDetails(OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_ShippingDetails(objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCustomersByStore(int storeId)
        {
            try
            {
                var result = _orderService.GetCustomersByStore(storeId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage UpdateOrder_TotalDetails(int order_id, int order_status_id, string comment)
        {
            try
            {
                var result = _orderService.UpdateOrder_TotalDetails(order_id, order_status_id, comment);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
