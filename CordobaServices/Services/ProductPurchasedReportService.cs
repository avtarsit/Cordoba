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
    }
}
