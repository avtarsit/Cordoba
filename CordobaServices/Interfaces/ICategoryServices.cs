using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICategoryServices
    {
        List<CategoryEntity> GetCategoryList(int Category_Id = 0);

        CategoryEntity GetCategoryById(int Category_Id);


        //Popular Category
        List<CategoryPopularEntity> GetCategoryListByStoreIdPopular(int storeID = 0);

        List<StoreEntity> GetStoreNameList();

        int InsertOrUpdateCategoryAsPopular(CategoryPopularEntity categoryPopular);

        int InsertOrUpdateCategory(CategoryEntity categoryEntity);

        List<CategoryEntity> GetParentCategoryList();
        //Get Language

        //Delete Category
        int DeleteCategory(int Category_Id);

        List<LanguageEntity> GetLanguageList();
        bool UpdateCategoryImage(int Category_Id, string fileName);

    }
}
