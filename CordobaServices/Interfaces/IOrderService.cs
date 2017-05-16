using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIA.HR.Api.Services.SearchHelpers;

namespace CordobaServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderEntity> GetOrderList(string sortColumn, TableParameter<OrderEntity> filter, string PageFrom = "");

        List<OrderEntity> GetOrderDetails(int orderId);

        int InsertOrderHistory(OrderHistoryEntity objHistoryEntity);
    }
}
