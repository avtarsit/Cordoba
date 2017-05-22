using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.SearchHelpers;

namespace CordobaServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderEntity> GetOrderList(string sortColumn, TableParameter<OrderEntity> filter, string PageFrom = "");

        List<OrderEntity> GetOrderDetails(int orderId);

        int InsertOrderHistory(OrderHistoryEntity objHistoryEntity);

        List<CustomerGroupEntity> GetCustomerGroupList();

        List<CurrencyEntity> GetCurrencyList();

        List<ZoneEntity> GetZoneListByCountry(int countryId);

        List<AddressEntity> GetCustomerAddress(int orderId);

        int UpdateOrder_CutomerDetails(OrderEntity objOrderEntity);

        int UpdateOrder_PaymentDetails(OrderEntity objOrderEntity);

        int UpdateOrder_ShippingDetails(OrderEntity objOrderEntity);

        List<CustomerEntity> GetCustomersByStore(int storeId);

        int UpdateOrder_TotalDetails(int order_id, int order_status_id, string comment);
    }
}
