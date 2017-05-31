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
    public class ProductPurchasedReportService : IProductPurchasedReportServices
    {

        private GenericRepository<ProductEntity> objGenericRepository = new GenericRepository<ProductEntity>();

        public List<OrderStatusEntity> GetOrderStatus(int language_id)
        {
            List<OrderStatusEntity> OrderStatus = new List<OrderStatusEntity>();
            try
            {
                var Result = objGenericRepository.ExecuteSQL<OrderStatusEntity>("GetOrderStatus", new SqlParameter {ParameterName = "language_id", DbType = DbType.Int32,Value = language_id }).ToList<OrderStatusEntity>();
                if (Result != null)
                    OrderStatus = Result.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OrderStatus;
        }


        public List<OrderProductEntity> GetProductPurchasedList(string sortColumn, int order_status_id, int store_id,TableParameter<OrderProductEntity> filter, DateTime? DateStart, DateTime? DateEnd)
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var paramOrderStatusId = new SqlParameter { ParameterName = "order_status_id", DbType = DbType.Int32, Value = order_status_id  };
                var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = store_id };
                var paramStartDate = new SqlParameter { ParameterName = "DateStart", DbType = DbType.DateTime, Value = DateStart ?? (object)DBNull.Value };
                var paramEndDate = new SqlParameter { ParameterName = "DateEnd", DbType = DbType.DateTime, Value = DateEnd ?? (object)DBNull.Value };
                var query = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetProductPurchasedList", paramOrderBy, paramPageSize, paramPageIndex, paramOrderStatusId, paramStoreId, paramStartDate, paramEndDate).ToList<OrderProductEntity>();
                return query;
            }
            catch (Exception ex )
            {
                throw ex ;
            }
        }

        public List<OrderProductEntity> GetProductViewedList(string sortColumn,  TableParameter<OrderProductEntity> filter)
        {
            try
            {
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                var query = objGenericRepository.ExecuteSQL<OrderProductEntity>("GetProductViewedList", paramOrderBy, paramPageSize, paramPageIndex).ToList<OrderProductEntity>();
                return query;
            }
            catch (Exception ex )
            {
                throw ex ;
            }
        }
    }
}
