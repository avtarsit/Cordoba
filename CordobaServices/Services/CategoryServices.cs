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
        private GenericRepository<CategoryEntity> CategoryEntityGenericRepository = new GenericRepository<CategoryEntity>();
        private GenericRepository<CategoryPopularEntity> CategoryPopularEntityGenericRepository = new GenericRepository<CategoryPopularEntity>();

      
        public List<CategoryEntity> GetCategoryList(int CategoryId = 0)
        {
            List<CategoryEntity> Categories = new List<CategoryEntity>();
            var ParameterCategoryId = new SqlParameter
            {
                ParameterName = "CategoryId",
                DbType = DbType.Int32,
                Value = CategoryId
            };
            var catalogueResult = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryList", ParameterCategoryId).ToList<CategoryEntity>();
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



        //Popular Category

        public List<CategoryPopularEntity> GetCategoryListByStoreIdPopular(int storeID = 0)
        {
            List<CategoryPopularEntity> PopularCategoryList = new List<CategoryPopularEntity>();
            if(storeID >= 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("StoreId", storeID);
                    PopularCategoryList = CategoryEntityGenericRepository.ExecuteSQL<CategoryPopularEntity>("EXEC GetCategoryListByStoreIdPopular", param).ToList<CategoryPopularEntity>().ToList();
                    
                }
                catch(Exception ex)
                {
                    throw;
                }
           }
            return PopularCategoryList;
        }


        public List<StoreEntity> GetStoreNameList()
        {
            try
            {
                var StoreList = CategoryEntityGenericRepository.ExecuteSQL<StoreEntity>("EXEC GetStoreNameList").ToList<StoreEntity>().ToList();
                return StoreList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int InsertOrUpdateCategoryAsPopular(CategoryPopularEntity categoryPopular)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("category_popularId", categoryPopular.category_popularId!=null ?categoryPopular.category_popularId:(object)DBNull.Value)
                    ,new SqlParameter("category_Id", categoryPopular.category_Id)
                    ,new SqlParameter("store_Id", categoryPopular.store_Id)
                    ,new SqlParameter("createdby", categoryPopular.createdby)
                };
            

                var result = CategoryPopularEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateCategoryAsPopular", param).ToList<int>().FirstOrDefault();



                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
