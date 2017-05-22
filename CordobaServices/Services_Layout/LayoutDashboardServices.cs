using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Interfaces_Layout;
using CordobaModels.Entities;
using System.Data.SqlClient;
using System.Data;
using CordobaModels;

namespace CordobaServices.Services_Layout
{
    public class LayoutDashboardServices : ILayoutDashboardServices
    {
        private GenericRepository<CategoryEntity> objGenericRepository = new GenericRepository<CategoryEntity>();

        public List<CategoryEntity> GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory)
        {
            List<CategoryEntity> CategoryList = new List<CategoryEntity>();
            var paramStoreId = new SqlParameter { ParameterName = "StoreId", DbType = DbType.Int32, Value = StoreID };
            var paramNeedToGetAllSubcategory = new SqlParameter { ParameterName = "NeedToGetAllSubcategory", DbType = DbType.Boolean, Value = NeedToGetAllSubcategory };
            CategoryList = objGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryListByStoreId", paramStoreId, paramNeedToGetAllSubcategory).ToList();
            return CategoryList;
        }

        public StoreEntity GetStoreDetailByUrl(String URL)
        {
            StoreEntity storeEntity = new StoreEntity();
            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("URL", URL) };
            storeEntity = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreDetailByUrl", sqlParameter).FirstOrDefault();
            return storeEntity;
        }

        public List<ProductEntity> GetLatestProductByStoreId(int StoreID)
        {           
            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
            var LatestProductList = objGenericRepository.ExecuteSQL<ProductEntity>("GetLatestProductByStoreId", sqlParameter).ToList();
            return LatestProductList;
        }

        public List<ProductEntity> GetPopularCategoryListByStoreId(int StoreID)
        {           
            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
            var LatestProductList = objGenericRepository.ExecuteSQL<ProductEntity>("GetLatestProductByStoreId", sqlParameter).ToList();
            return LatestProductList;
        }

    }
}
