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
        List<CustomerEntity> GetCustomerList(int StoreId, int LoggedInUserId, string sortColumn, TableParameter<CustomerEntity> tableParameter, string customerName, string email, int? customer_group_id,int? status, int? approved, string ip, DateTime? date_added , int? storeId);
        
        CustomerEntity GetCustomerById(int StoreId, int LoggedInUserId, int customer_id);

        int InsertUpdateCustomer(int LoggedInUserId, CustomerEntity customerEntity);

        int DeleteCustomer(int StoreId, int LoggedInUserId, int customer_id);

        int CustomerImport(int store_id,int LoggedInUserId, int customer_group_id, DataTable CustomerTable);

        int PointsImporter(int store_id, int LoggedInUserId, bool IsSendEmail, DataTable PointsTable);
    }
}
