using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CordobaModels.Entities;
using CordobaServices.Helpers;

namespace CordobaAPI.API
{ 
    public class ProductPurchasedReportApiController : ApiController
    {

        public IProductPurchasedReportServices _productPurchasedReportService;
        public ProductPurchasedReportApiController()
        {
            _productPurchasedReportService = new ProductPurchasedReportService();
        }
        [HttpGet]
        public HttpResponseMessage GetOrderStatus(int language_id)
        {
            try
            {
                var result = _productPurchasedReportService.GetOrderStatus(language_id);
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
        public TableParameter<OrderProductEntity> GetProductPurchasedList(int PageIndex, int order_status_id, int store_id, DateTime? DateStart, DateTime? DateEnd, TableParameter<OrderProductEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _productPurchasedReportService.GetProductPurchasedList(sortColumn, order_status_id, store_id, tableParameter, DateStart, DateEnd).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<OrderProductEntity>
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


        [HttpPost]
        public TableParameter<OrderProductEntity> GetProductViewedList(int PageIndex, TableParameter<OrderProductEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _productPurchasedReportService.GetProductViewedList(sortColumn, tableParameter).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<OrderProductEntity>
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

    }
}