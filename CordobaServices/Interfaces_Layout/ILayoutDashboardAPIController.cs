using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces_Layout
{
    public interface ILayoutDashboardServices
    {
        List<CategoryEntity> GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory);
        StoreEntity GetStoreDetailByUrl(String URL);
        List<ProductEntity> GetLatestProductByStoreId(int StoreID);
        List<CategoryPopularEntity> GetPopularCategoryListByStoreId(int StoreID);
        CustomerEntity CustomerLogin(CustomerEntity CustomerObj);
        int? AddtoWishList(wishlistEntity WishlistObj);
    }
}
