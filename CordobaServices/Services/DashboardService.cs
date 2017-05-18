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
    public class DashboardService : IDashboardService
    {
        private GenericRepository<OrderEntity> objGenericRepository = new GenericRepository<OrderEntity>();

        public List<OrderEntity> GetLatestOrderDetailsDashboard(int storeId)
        {
            List<OrderEntity> orders = new List<OrderEntity>();
            var paramOrderId = new SqlParameter { ParameterName = "order_id", DbType = DbType.Int32, Value = storeId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetLatestOrderDetailsDashboard", paramOrderId).ToList();
            return orders;
        }
       
        
    }
}
