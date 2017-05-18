using CordobaModels;
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
    public class CustomerGroupService : ICustomerGroupService
    {
        private GenericRepository<CustomerGroupEntity> CustomerGroupEntityGenericRepository = new GenericRepository<CustomerGroupEntity>();

        public List<CustomerGroupEntity> GetCustomerGroupList()
        {
            try
            {
                var CustomerGroupList = CustomerGroupEntityGenericRepository.ExecuteSQL<CustomerGroupEntity>("EXEC GetCustomerGroupList").ToList<CustomerGroupEntity>().ToList();
                return CustomerGroupList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CustomerGroupEntity GetCustomerGroupDetail(int customerGroupID = 0)
        {
            CustomerGroupEntity CustomerGroupDetail = new CustomerGroupEntity();
            if (customerGroupID > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("customer_group_id", customerGroupID);
                    CustomerGroupDetail = CustomerGroupEntityGenericRepository.ExecuteSQL<CustomerGroupEntity>("EXEC GetCustomerGroupDetails ", param ).ToList<CustomerGroupEntity>().FirstOrDefault();

                }
                catch (Exception ex)
                {

                    throw;
                }

            }

            return CustomerGroupDetail;

        }

        public int CreateOrUpdateCustomerGroup(CustomerGroupEntity customerGroup)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("customer_group_id", customerGroup.customer_group_id);
                param[1] = new SqlParameter("name", customerGroup.name);

                var result = CustomerGroupEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateCustomerGroup" , param).ToList<int>().FirstOrDefault();

               

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteCustomerGroup(int CustomerGroupId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("customer_group_id", CustomerGroupId);

                var Result = CustomerGroupEntityGenericRepository.ExecuteSQL<int>("EXEC DeleteCustomerGroup ", param).ToList<int>().FirstOrDefault();
                return Result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
