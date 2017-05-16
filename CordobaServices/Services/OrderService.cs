using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIA.HR.Api.Services.SearchHelpers;

namespace CordobaServices.Services
{
    public class OrderService : IOrderService
    {
        private GenericRepository<OrderEntity> objGenericRepository = new GenericRepository<OrderEntity>();

        public List<OrderEntity> GetOrderDetails(int orderId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderDetails", paramOrderId).ToList();

            if (orders != null && orders.Count != 0)
            {
                List<OrderProductEntity> orderProducts = new List<OrderProductEntity>();
                var paramOrderProductOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderProducts = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetOrderProductByOrderId", paramOrderProductOrderId).ToList();

                List<OrderHistoryEntity> orderHistory = new List<OrderHistoryEntity>();
                var paramOrderHistoryOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = orderId };
                orderHistory = objGenericRepository.ExecuteSQL<OrderHistoryEntity>("GetOrderHistoryByOrderId", paramOrderHistoryOrderId).ToList();

                orders[0].orderProductEntity = orderProducts;
                orders[0].orderHistoryEntity = orderHistory;
            }

            return orders;
        }

        public int InsertOrderHistory(OrderHistoryEntity objHistoryEntity)
        {
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = objHistoryEntity.order_id };
            var paramnotify = new SqlParameter { ParameterName = "notify", DbType = DbType.Int32, Value = objHistoryEntity.notify };
            var paramcomment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = objHistoryEntity.comment };
            var paramorderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = objHistoryEntity.order_status_id };
            int result = objGenericRepository.ExecuteSQL<int>("InsertOrderHistory", paramOrderId, paramnotify, paramcomment, paramorderStatusId).FirstOrDefault();
            return result;
        }

        public IEnumerable<OrderEntity> GetOrderList(string sortColumn, TableParameter<OrderEntity> filter, string PageFrom = "")
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var paramPageFrom = new SqlParameter { ParameterName = "PageFrom", DbType = DbType.String, Value = PageFrom };
                var query = objGenericRepository.ExecuteSQL<OrderEntity>("GetOrderList", paramOrderBy, paramPageSize, paramPageIndex, paramPageFrom).AsQueryable();
                //if (filter.SortColumn != null)
                //{
                //    query = query.DynamicOrderBy(filter.SortColumn.Column, filter.SortColumn.Desc);
                //}
                //else
                //{
                //    query = query.OrderBy(j => j.order_id);
                //}
                return query;
            }
            catch (Exception)
            {
                throw;
            }
            
            //return result;
        }
    }
}
