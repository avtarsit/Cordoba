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
        private GenericRepository<LanguageEntity> LanguageEntityGenericRepository = new GenericRepository<LanguageEntity>();

      
        public List<CategoryEntity> GetCategoryList(int Category_Id = 0)
        {
            List<CategoryEntity> Categories = new List<CategoryEntity>();
            var ParameterCategory_Id = new SqlParameter
            {
                ParameterName = "Category_Id",
                DbType = DbType.Int32,
                Value = Category_Id
            };
            var catalogueResult = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryList", ParameterCategory_Id).ToList<CategoryEntity>();
            if (catalogueResult != null)
                Categories = catalogueResult.ToList();
            return Categories;
        }

        public CategoryEntity GetCategoryById(int Category_Id = 0, int language_id = 0)
        {
            CategoryEntity categoryEntity = new CategoryEntity();
            if (Category_Id > 0)
            { 
                try
                {
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("Category_Id", Category_Id);
                    param[1] = new SqlParameter("language_id", language_id);
                    categoryEntity = CategoryEntityGenericRepository.ExecuteSQL<CategoryEntity>("EXEC GetCategoryById ", param).ToList<CategoryEntity>().FirstOrDefault();

                }
                catch (Exception ex)
                {

                    throw;
                }
            
            }
            return categoryEntity;
            //if (Category_Id > 0)
            //{
            //    categoryEntity = GetCategoryList(Category_Id).FirstOrDefault();
            //}
            //else
            //{
            //    categoryEntity = new CategoryEntity();
            //}
            //CategoryStoreEntity CategoryStoreList = new CategoryStoreEntity();
            //List<StoreEntity> StoreList = new List<StoreEntity>();
            //CategoryStoreList.Category_Id = Category_Id;
            //CategoryStoreList.CategoryStore = StoreList;
            //categoryEntity.CategoryStoreList = CategoryStoreList;
            //return categoryEntity;

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




        // Language get

        public LanguageEntity GetLanguageList(int language_id = 0)
        {
            LanguageEntity languageEntity = new LanguageEntity();
            if (language_id > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("language_id", language_id);
                    //param[1] = new SqlParameter("name", name);
                    //param[2] = new SqlParameter("language_id", image);
                    languageEntity = LanguageEntityGenericRepository.ExecuteSQL<LanguageEntity>("EXEC GetLanguageList ", param).ToList<LanguageEntity>().FirstOrDefault();

                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return languageEntity;

        }




    }
}
