using CordobaAPI.Utility;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
   
    public class CustomerService : ICustomerService
    {
        private GenericRepository<CustomerEntity> CustomerEntityGenericRepository = new GenericRepository<CustomerEntity>();

        public List<CustomerEntity> GetCustomerList()
        {
            try
            {
                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerList").ToList<CustomerEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
