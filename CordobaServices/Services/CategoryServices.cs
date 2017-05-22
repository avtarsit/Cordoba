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
    public class CategoryService : ICategoryServices
    {
        private GenericRepository<CategoryEntity> objGenericRepository = new GenericRepository<CategoryEntity>();
        public List<CategoryEntity> GetCategoryList(int CategoryId = 0)
        {
            List<CategoryEntity> Categories = new List<CategoryEntity>();
            var ParameterCategoryId = new SqlParameter
            {
                ParameterName = "CategoryId",
                DbType = DbType.Int32,
                Value = CategoryId
            };
            var catalogueResult = objGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryList", ParameterCategoryId).ToList<CategoryEntity>();
            if (catalogueResult != null)
                Categories = catalogueResult.ToList();
            return Categories;
        }

        public CategoryEntity GetCategoryById(int CategoryId = 0)
        {
            CategoryEntity categoryEntity = new CategoryEntity();
            if (CategoryId > 0)
            {
                categoryEntity = GetCategoryList(CategoryId).FirstOrDefault();
            }
            else
            {
                categoryEntity = new CategoryEntity();
            }
            CategoryStoreEntity CategoryStoreList = new CategoryStoreEntity();
            List<StoreEntity> StoreList = new List<StoreEntity>();
            CategoryStoreList.CategoryId = CategoryId;
            CategoryStoreList.CategoryStore = StoreList;
            categoryEntity.CategoryStoreList = CategoryStoreList;
            return categoryEntity;

        }






    }
}
