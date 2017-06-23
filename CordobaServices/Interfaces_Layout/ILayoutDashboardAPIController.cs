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
        List<ProductEntity> GetHotDealsListByStoreId(int StoreID);
        CustomerEntity CustomerLogin(CustomerEntity CustomerObj);
        int? AddtoWishList(wishlistEntity WishlistObj);
        int? RemoveFromWishList(int storeid, int product_id, int customer_Id);
        List<ProductEntity> GetSpecialOfferListByStoreId(int StoreID);
        CustomerEntity CustomerDetailLayout(int CustomerId, int StoreId);
        int? SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj);
        int? SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj);
        List<AddressEntity> GetCustomerAddressList_Layout(int StoreId, int customer_id);
        List<AddressEntity> AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj);
        int? DeleteCustomerAddress(int StoreId, int customer_id, int address_id);
        List<StoreImageEntity> GetStoreImageList(int Store_Id);
        List<ProductEntity> GetBestSellerListByStoreId(int StoreID);
        List<StoreTermsEntity> GetStoreTermsDetail(int Store_Id);
        List<OrderDetailCountEntity> GetOrderStatusCount(int StoreID);
    }
}
