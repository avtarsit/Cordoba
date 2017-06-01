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
            var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetLatestProductByStoreId", sqlParameter).ToList();
            return result;
        }

        public List<CategoryPopularEntity> GetPopularCategoryListByStoreId(int StoreID)
        {           
            SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
            var result = objGenericRepository.ExecuteSQL<CategoryPopularEntity>("GetPopularCategoryListByStoreId_Dashboard", sqlParameter).ToList();
            return result;
        }

        public CustomerEntity CustomerLogin(CustomerEntity CustomerObj)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("email", CustomerObj.email)
               ,new SqlParameter("password", CustomerObj.password)
               ,new SqlParameter("cartgroup_id", CustomerObj.cartgroup_id)
               ,new SqlParameter("store_id", CustomerObj.store_id)
            };
            var result = objGenericRepository.ExecuteSQL<CustomerEntity>("CustomerLogin", sqlParameter).FirstOrDefault();
            return result;
        }


        public int? AddtoWishList(wishlistEntity WishlistObj)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("customer_id", WishlistObj.customer_id)
               ,new SqlParameter("store_id", WishlistObj.store_id)
               ,new SqlParameter("product_id", WishlistObj.product_id)               
            };
            var result = objGenericRepository.ExecuteSQL<int>("AddtoWishList", sqlParameter).FirstOrDefault();
            return result;
        }

    }
}
