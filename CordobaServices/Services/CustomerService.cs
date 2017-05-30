﻿
using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Helpers;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
   
    public class CustomerService : ICustomerService
    {
        private GenericRepository<CustomerEntity> CustomerEntityGenericRepository = new GenericRepository<CustomerEntity>();

        public List<CustomerEntity> GetCustomerList(string sortColumn, TableParameter<CustomerEntity> filter, string customerName, string email, int? customer_group_id,int? status, int? approved, string ip, DateTime? date_added)
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };

                var paramCustomerName = new SqlParameter { ParameterName = "customerName", DbType = DbType.String, Value = customerName ?? DBNull.Value.ToString() };
                var paramEmail = new SqlParameter { ParameterName = "email", DbType = DbType.String, Value = email ?? (object)DBNull.Value };
                var paramCustomer_group_id = new SqlParameter { ParameterName = "customer_group_id", DbType = DbType.Int32, Value = customer_group_id ?? (object)DBNull.Value };
                var paramApproved = new SqlParameter { ParameterName = "approved", DbType = DbType.Int32, Value = approved ?? (object)DBNull.Value };
                var paramIp = new SqlParameter { ParameterName = "ip", DbType = DbType.String, Value = ip ?? (object)DBNull.Value };
                var paramDate_added = new SqlParameter { ParameterName = "date_added", DbType = DbType.DateTime, Value = date_added ?? (object)DBNull.Value };
                var paramstatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = status ?? (object)DBNull.Value };
                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerList", paramOrderBy, paramPageSize, paramPageIndex, paramCustomerName, paramEmail, paramCustomer_group_id, paramApproved, paramIp, paramDate_added, paramstatus).ToList<CustomerEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CustomerEntity GetCustomerById(int customer_id)
        {
            try
            {
                CustomerEntity customerEntity = new CustomerEntity();
                List<AddressEntity> addressEntity = new List<AddressEntity>();
                if(customer_id > 0)
                {
                    var paramCustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                    var result = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerById", paramCustomer_id).FirstOrDefault();
                    if(result != null)
                    {
                        customerEntity = result;
                    }


                    #region customer Address
                    var AddressResult = CustomerEntityGenericRepository.ExecuteSQL<AddressEntity>("EXEC GetCustomerAddressList", new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id }).ToList<AddressEntity>().ToList();
                    if (AddressResult != null)
                    {
                        addressEntity = AddressResult;
                    }
                    customerEntity.AddressList = addressEntity;
                    #endregion 
                }
                else
                {
                    customerEntity = new CustomerEntity();
                    customerEntity.AddressList = addressEntity = new List<AddressEntity>();
                }

               


                return customerEntity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
