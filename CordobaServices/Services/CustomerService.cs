
using CordobaCommon;
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

        public List<CustomerEntity> GetCustomerList(string sortColumn, TableParameter<CustomerEntity> filter, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int? storeId)
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
                var paramStoreId = new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId ?? (object)DBNull.Value };
                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerList", paramOrderBy, paramPageSize, paramPageIndex, paramCustomerName, paramEmail, paramCustomer_group_id, paramApproved, paramIp, paramDate_added, paramstatus, paramStoreId).ToList<CustomerEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CustomerEntity GetCustomerById(int StoreId, int LoggedInUserId, int customer_id)
        {
            try
            {
                CustomerEntity customerEntity = new CustomerEntity();
                List<AddressEntity> addressEntity = new List<AddressEntity>();
                List<PointsAuditEntity> PointsAuditList = new List<PointsAuditEntity>();

                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };

                if (customer_id > 0)
                {
                    var paramCustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                    var result = CustomerEntityGenericRepository.ExecuteSQL<CustomerEntity>("EXEC GetCustomerById", paramCustomer_id).FirstOrDefault();
                    if (result != null)
                    {
                        customerEntity = result;
                        customerEntity.password = Security.Decrypt(result.password);
                    }


                    #region customer Address
                    var AddressResult = CustomerEntityGenericRepository.ExecuteSQL<AddressEntity>("EXEC GetCustomerAddressList", new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id }).ToList<AddressEntity>().ToList();
                    if (AddressResult != null)
                    {
                        addressEntity = AddressResult;
                    }

                    #endregion

                    #region  PointsAuditEntity

                    var PointsAuditResult = CustomerEntityGenericRepository.ExecuteSQL<PointsAuditEntity>("EXEC GetPointsAuditDetail", new SqlParameter { ParameterName = "Customer_Id", DbType = DbType.Int32, Value = customer_id }).ToList<PointsAuditEntity>().ToList();
                    if (AddressResult != null)
                    {
                        PointsAuditList = PointsAuditResult;
                    }
                    #endregion
                    customerEntity.AddressList = addressEntity;
                    customerEntity.PointsAuditList = PointsAuditList;
                }
                else
                {
                    customerEntity = new CustomerEntity();
                    customerEntity.AddressList = addressEntity = new List<AddressEntity>();
                    customerEntity.PointsAuditList = new List<PointsAuditEntity>();
                }




                return customerEntity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }



        public int InsertUpdateCustomer(int LoggedInUserId, CustomerEntity customerEntity)
        {
            string AddressXml = Helpers.ConvertToXml<AddressEntity>.GetXMLString(customerEntity.AddressList);

            string PointsAuditXml = Helpers.ConvertToXml<PointsAuditEntity>.GetXMLString(customerEntity.PointsAuditList);

            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                                                   
                                                   new SqlParameter("customer_id", customerEntity.customer_id)
                                                 , new SqlParameter("store_id", customerEntity.store_id ?? (object) DBNull.Value)
                                                 , new SqlParameter("firstname", customerEntity.firstname ??  DBNull.Value.ToString())
                                                 , new SqlParameter("lastname", customerEntity.lastname ??  DBNull.Value.ToString())
                                                 , new SqlParameter("email", customerEntity.email ??  DBNull.Value.ToString())
                                                 , new SqlParameter("telephone", customerEntity.telephone ??   DBNull.Value.ToString())
                                                 , new SqlParameter("password", Security.Encrypt(customerEntity.password) ??  DBNull.Value.ToString())
                                                 , new SqlParameter("status", customerEntity.status)
                                                 , new SqlParameter("approved", customerEntity.approved)
                                                 , new SqlParameter("activated", customerEntity.activated)
                                                 , new SqlParameter("is_admin", customerEntity.is_admin)
                                                 , new SqlParameter("customer_department_id", customerEntity.customer_department_id.HasValue?customerEntity.customer_department_id.Value :(object)DBNull.Value)
                                                 , new SqlParameter("AddressXml", AddressXml ??  (object)DBNull.Value)
                                                 , new SqlParameter("PointsAuditXml", PointsAuditXml ??  (object)DBNull.Value)
                                                };
            int result = CustomerEntityGenericRepository.ExecuteSQL<int>("InsertUpdateCustomer", sqlParameter).FirstOrDefault();
            return result;
        }




        public int DeleteCustomer(int StoreId, int LoggedInUserId, int customer_id)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramId = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                int result = CustomerEntityGenericRepository.ExecuteSQL<int>("DeleteCustomer", paramId).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int PointsImporter(int store_id, int LoggedInUserId, bool IsSendEmail, DataTable PointsTable)
        {
            string PointsXml = GeneralMethods.ConvertDatatableToXML(PointsTable);
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("store_id", store_id);
                param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[2] = new SqlParameter("IsSendEmail", IsSendEmail);
                param[3] = new SqlParameter("PointsXml", PointsXml);

                var result = CustomerEntityGenericRepository.ExecuteSQL<int>("EXEC ImportPointsXml", param).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public string CustomerImport(int store_id, int LoggedInUserId, int customer_group_id, DataTable CustomerTable)
        {
            string CustomerXml = GeneralMethods.ConvertDatatableToXML(CustomerTable);
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("store_id", store_id);
                param[1] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[2] = new SqlParameter("customer_group_id", customer_group_id);
                param[3] = new SqlParameter("CustomerXml", CustomerXml);

                var result = CustomerEntityGenericRepository.ExecuteSQL<dynamic>("EXEC ImportCustomerXml", param).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UploadUserImage(int customerImage_id, int customer_id, string ImageName)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customerImage_id", customerImage_id),
                    new SqlParameter("customer_id", customer_id),
                    new SqlParameter("image", ImageName)
                };
                int result = CustomerEntityGenericRepository.ExecuteSQL<int>("AddOrUpdateCustomerImage", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CustomerImageEntity> getUserImage(int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("customer_id", customer_id),
                 };

                var CustomerList = CustomerEntityGenericRepository.ExecuteSQL<CustomerImageEntity>("GetUserImage", sqlParameter).ToList<CustomerImageEntity>().ToList();
                return CustomerList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int deleteCustomerImage(int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("customer_id", customer_id)                   
                };
                var result = CustomerEntityGenericRepository.ExecuteSQL<int>("DeleteCustomerImage", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int InsertPointAudit(int customer_id, string description, int points)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                    new SqlParameter("customer_id", customer_id),
                    new SqlParameter("description", description),
                    new SqlParameter("points", points)
                };
                var result = CustomerEntityGenericRepository.ExecuteSQL<int>("InsertPointAudit", sqlParameter).FirstOrDefault();
                return result;
            }
            catch(Exception e)
            {
                return 0;
            }
        }


        

    }
}
