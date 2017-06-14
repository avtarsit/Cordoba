using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerEntity> GetCustomerList(string sortColumn, TableParameter<CustomerEntity> tableParameter, string customerName, string email, int? customer_group_id,int? status, int? approved, string ip, DateTime? date_added);
        CustomerEntity GetCustomerById(int customer_id);

        int InsertUpdateCustomer(CustomerEntity customerEntity);

        int DeleteCustomer(int customer_id);

        int CustomerImport(int store_id, int customer_group_id, DataTable CustomerTable);

        int PointsImporter(int store_id, bool IsSendEmail, DataTable PointsTable);
    }
}
