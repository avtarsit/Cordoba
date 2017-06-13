using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers;

namespace CordobaServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderEntity> GetOrderList(string sortColumn, int? orderId, int? order_status_id, string CustomerName, decimal? total, Nullable<DateTime> DateAdded, Nullable<DateTime> DateModified, TableParameter<OrderEntity> filter, string PageFrom = "");

        List<OrderEntity> GetOrderDetails(int orderId);

        List<OrderEntity> GetOrderHistory(int customer_Id);

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
