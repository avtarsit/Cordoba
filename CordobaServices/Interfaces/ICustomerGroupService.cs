using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICustomerGroupService
    {
        List<CustomerGroupEntity> GetCustomerGroupList();
        CustomerGroupEntity GetCustomerGroupDetail(int currencyID = 0);

        int CreateOrUpdateCustomerGroup(CustomerGroupEntity customerGroup);

        int DeleteCustomerGroup(int CustomerGroupId);
    }
}
