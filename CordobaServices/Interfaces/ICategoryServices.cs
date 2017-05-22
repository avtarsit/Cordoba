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
        List<CategoryEntity> GetCategoryList(int CategoryId = 0);

        CategoryEntity GetCategoryById(int CategoryId);


        //Popular Category
        List<CategoryEntity> GetCategoryListByStoreIdPopular(int storeID = 0);

        List<CategoryEntity> GetStoreNameList();

        //int CreateOrUpdatePopularCategory(CategoryEntity popularCategory);

        //int DeletePopularCategory(int CategoryId);
    }
}
