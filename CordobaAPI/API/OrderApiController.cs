using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TIA.HR.Api.Services.SearchHelpers;

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
                //var totalRecordes = result.Count();

                //var filteredRecords = result.Skip(tableParameter.iDisplayStart)
                //    .Take(tableParameter.iDisplayLength).ToList();

                //return Request.CreateResponse(HttpStatusCode.OK, result);

                return new TableParameter<OrderEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };

                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
