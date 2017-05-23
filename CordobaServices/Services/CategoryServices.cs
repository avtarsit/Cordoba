﻿using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
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
            //Categories.Add(new CategoryEntity() { CategoryId = 1, name = "Home & Garden", SortOrder = 1 });
            //Categories.Add(new CategoryEntity() { CategoryId = 2, name= "Home Appliances", SortOrder = 2 });
            //Categories.Add(new CategoryEntity() { CategoryId = 3, name= "Health & Safety", SortOrder = 3 });
            //Categories.Add(new CategoryEntity() { CategoryId = 4, name= "Fashion", SortOrder = 4 });
            //Categories.Add(new CategoryEntity() { CategoryId = 5, name= "Flowers", SortOrder = 5 });
            //Categories.Add(new CategoryEntity() { CategoryId = 6, name= "Gaming", SortOrder = 6 });
            //Categories.Add(new CategoryEntity() { CategoryId = 7, name= "Gardening", SortOrder = 7 });
            //Categories.Add(new CategoryEntity() { CategoryId = 8, name= "Gift Cards", SortOrder = 8 });
            //Categories.Add(new CategoryEntity() { CategoryId = 9, name= "Giftware", SortOrder = 9 });
            //Categories.Add(new CategoryEntity() { CategoryId = 10,name = "Home & Garden", SortOrder = 10 });
            //Categories.Add(new CategoryEntity() { CategoryId = 11,name = "Home Appliances", SortOrder = 11 });
            //Categories.Add(new CategoryEntity() { CategoryId = 12,name = "Home and Garden", SortOrder = 12 });
            //Categories.Add(new CategoryEntity() { CategoryId = 13,name = "Nursery", SortOrder = 13 });
            //Categories.Add(new CategoryEntity() { CategoryId = 14,name = "Personal Care", SortOrder = 14 });
            //Categories.Add(new CategoryEntity() { CategoryId = 15,name = "Personal Items", SortOrder = 15 });
            return Categories;
        }

        public CategoryEntity GetCategoryById(int CategoryId = 0)
        {
            CategoryEntity categoryEntity = new CategoryEntity();
            if (CategoryId > 0)
            {
                //categoryEntity = (from t in GetCategoryList(CategoryId)
                //                  where t.CategoryId == CategoryId
                //                select t).FirstOrDefault();
            }
            else
            {
                categoryEntity = new CategoryEntity();
            }
            CategoryStoreEntity CategoryStoreList = new CategoryStoreEntity();
            List<StoreEntity> StoreList = new List<StoreEntity>();
            //StoreList.Add(new StoreEntity() { StoreID = 0, StoreName = "Default", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 1, StoreName = "Make a Difference Thank You AE", IsSelected = true });
            //StoreList.Add(new StoreEntity() { StoreID = 2, StoreName = "Arkle Finance rewards 2015", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 3, StoreName = "Make a Difference Thank You AU", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 4, StoreName = "Make a Difference Thank You CA", IsSelected = true });
            //StoreList.Add(new StoreEntity() { StoreID = 5, StoreName = "Make a Difference Thank You FR", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 6, StoreName = "Make a Difference Thank You JP", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 7, StoreName = "Make a Difference Thank You IN", IsSelected = true });
            //StoreList.Add(new StoreEntity() { StoreID = 8, StoreName = "Make a Difference Thank You NZ", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 9, StoreName = "Make a Difference Thank You US", IsSelected = false });
            //StoreList.Add(new StoreEntity() { StoreID = 10, StoreName = "Annodata rewards", IsSelected = false });

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
