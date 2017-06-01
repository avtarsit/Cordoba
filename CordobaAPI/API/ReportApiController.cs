using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Helpers;

namespace CordobaAPI.API
{
    public class ReportApiController : ApiController
    {
        public IReportService _reportServices;

        public ReportApiController()
        {
            _reportServices = new ReportService();
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetReturnList(int PageIndex, DateTime DateStart, DateTime DateEnd, int GroupById, int StatusId , TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetReturnList(sortColumn, DateStart, DateEnd, GroupById, StatusId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
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