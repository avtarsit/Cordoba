using CordobaModels.Entities;
using CordobaServices.Helpers;
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
    public class CustomerApiController : ApiController
    {
        public ICustomerService _CustomerService;

        public CustomerApiController()
        {
            _CustomerService = new CustomerService();
        }


        [HttpPost]
        public TableParameter<CustomerEntity> GetCustomerList(int PageIndex, string customerName, string email, int? customer_group_id,int? status , int? approved, string ip, DateTime? date_added, TableParameter<CustomerEntity> tableParameter)
        {
            try
            {

                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _CustomerService.GetCustomerList(sortColumn, tableParameter, customerName, email, customer_group_id,status, approved, ip, date_added).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<CustomerEntity>
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
