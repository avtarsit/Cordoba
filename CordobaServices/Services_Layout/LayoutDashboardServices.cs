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

    }
}
