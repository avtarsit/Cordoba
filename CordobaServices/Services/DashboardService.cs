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
            var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = storeId };
            orders = objGenericRepository.ExecuteSQL<OrderEntity>("GetLatestOrderDetailsDashboard", paramStoreId).ToList();
            return orders;
        }

        public DashboardSummaryEntity GetDashboardTopHeaderFields(int storeId)
        {
            var paramStoreId = new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId };
            var dashboardHeaderFields = objGenericRepository.ExecuteSQL<DashboardSummaryEntity>("GetDashboardTopHeaderFields", paramStoreId).SingleOrDefault();
            return dashboardHeaderFields;
        }

        public DashboardSummaryEntity GetDashboardSummaryCharts(int storeId, int ChartFiltertype)
        {
            DashboardSummaryEntity objDashboardSummaryEntity = new DashboardSummaryEntity();
            var paramStoreId = new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId };
            var objDashboardOrderSummary = objGenericRepository.ExecuteSQL<DashboardOrderSummary>("GetDashboardOrderSummary_Chart", paramStoreId).ToList();

            var objDashboardSalesAnalytics = objGenericRepository.ExecuteSQL<DashboardSalesAnalytics>("GetDashboardSalesAnalytics_Chart", new SqlParameter { ParameterName = "ChartFiltertype", DbType = DbType.Int32, Value = ChartFiltertype }).ToList();

            var objDashboardTopSellStore = objGenericRepository.ExecuteSQL<DashboardTopSellStore>("GetDashboardTop5_SellStore_Chart", new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId }).ToList();

            var objDashboardTopPurchaseProduct = objGenericRepository.ExecuteSQL<DashboardTopPurchaseProduct>("GetDashboardTop5_PurchaseProduct_Chart", new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId }).ToList();

            var objDashboardTopCustomer = objGenericRepository.ExecuteSQL<DashboardTopCustomer>("GetDashboardTop5_Customers_Chart", new SqlParameter { ParameterName = "storeId", DbType = DbType.Int32, Value = storeId }).ToList();

            objDashboardSummaryEntity.dashboardOrderSummary = objDashboardOrderSummary;
            objDashboardSummaryEntity.dashboardSalesAnalytics = objDashboardSalesAnalytics;
            objDashboardSummaryEntity.dashboardTopSellStore = objDashboardTopSellStore;
            objDashboardSummaryEntity.dashboardTopPurchaseProduct = objDashboardTopPurchaseProduct;
            objDashboardSummaryEntity.dashboardTopCustomer = objDashboardTopCustomer;
            return objDashboardSummaryEntity;
        }
    }
}
