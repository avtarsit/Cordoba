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
            try
            {
                List<CategoryEntity> CategoryList = new List<CategoryEntity>();
                var paramStoreId = new SqlParameter { ParameterName = "StoreId", DbType = DbType.Int32, Value = StoreID };
                var paramNeedToGetAllSubcategory = new SqlParameter { ParameterName = "NeedToGetAllSubcategory", DbType = DbType.Boolean, Value = NeedToGetAllSubcategory };
                CategoryList = objGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryListByStoreId", paramStoreId, paramNeedToGetAllSubcategory).ToList();
                return CategoryList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public StoreEntity GetStoreDetailByUrl(String URL)
        {
            try
            {
                StoreEntity storeEntity = new StoreEntity();
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("URL", URL) };
                storeEntity = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreDetailByUrl", sqlParameter).FirstOrDefault();
                return storeEntity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetLatestProductByStoreId(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetLatestProductByStoreId", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CategoryPopularEntity> GetPopularCategoryListByStoreId(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<CategoryPopularEntity>("GetPopularCategoryListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetHotDealsListByStoreId(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetHotDealsListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetSpecialOfferListByStoreId(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetSpecialOfferListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CustomerEntity CustomerLogin(CustomerEntity CustomerObj)
        {
            try
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
            catch (Exception)
            {

                throw;
            }

        }


        public int? AddtoWishList(wishlistEntity WishlistObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("customer_id", WishlistObj.customer_id)
               ,new SqlParameter("store_id", WishlistObj.store_id)
               ,new SqlParameter("product_id", WishlistObj.product_id)               
            };
                var result = objGenericRepository.ExecuteSQL<int>("AddtoWishList", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int? RemoveFromWishList(int storeid, int product_id, int customer_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("customer_id",customer_Id)
               ,new SqlParameter("store_id",storeid)
               ,new SqlParameter("product_id",product_id)               
            };
                var result = objGenericRepository.ExecuteSQL<int>("RemoveFromWishList", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public CustomerEntity CustomerDetailLayout(int CustomerId, int StoreId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("CustomerId", CustomerId)
               ,new SqlParameter("StoreId",StoreId)              
            };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("CustomerDetail_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int? SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",CustomerObj.customer_id)  
             , new SqlParameter("firstname",CustomerObj.firstname)  
             , new SqlParameter("lastname",CustomerObj.lastname)     
             , new SqlParameter("email",CustomerObj.email)     
             , new SqlParameter("telephone",CustomerObj.telephone!=null ?CustomerObj.telephone:(object) DBNull.Value )     
            };
                var result = objGenericRepository.ExecuteSQL<int>("UpdateCustomerBasicDetails_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public int? SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",CustomerObj.customer_id)  
             , new SqlParameter("password",CustomerObj.password)                 
            };
                var result = objGenericRepository.ExecuteSQL<int>("UpdateChangedPassword_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public List<AddressEntity> GetCustomerAddressList_Layout(int StoreId, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",customer_id)                            
            };
                var result = objGenericRepository.ExecuteSQL<AddressEntity>("GetCustomerAddressList_Layout", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<AddressEntity> AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("StoreId",StoreId) 
             , new SqlParameter("address_id",AddressObj.address_id)
             , new SqlParameter("customer_id",AddressObj.customer_id)  
             , new SqlParameter("company",AddressObj.company!=null ?AddressObj.company:(object)DBNull.Value)  
             , new SqlParameter("firstname",AddressObj.firstname!=null? AddressObj.firstname:(object)DBNull.Value)  
             , new SqlParameter("lastname",AddressObj.lastname!=null? AddressObj.lastname:(object)DBNull.Value)  
             , new SqlParameter("address_1",AddressObj.address_1!=null? AddressObj.address_1:(object)DBNull.Value)  
             , new SqlParameter("address_2",AddressObj.address_2!=null? AddressObj.address_2:(object)DBNull.Value)  
             , new SqlParameter("postcode",AddressObj.postcode!=null? AddressObj.postcode:(object)DBNull.Value)  
             , new SqlParameter("city",AddressObj.city!=null? AddressObj.city:(object)DBNull.Value)  
             , new SqlParameter("country_id",AddressObj.country_id) 
             , new SqlParameter("IsDefaultAddress",AddressObj.IsDefaultAddress!=null?AddressObj.IsDefaultAddress:false) 
            };
                var result = objGenericRepository.ExecuteSQL<AddressEntity>("AddOrUpdateAddressDetail_Layout", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int? DeleteCustomerAddress(int StoreId, int customer_id, int address_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",customer_id)                            
             , new SqlParameter("address_id",address_id) 
            };
                var result = objGenericRepository.ExecuteSQL<int>("DeleteCustomerAddress_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StoreImageEntity> GetStoreImageList(int Store_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("Store_Id",Store_Id)
            };
                var result = objGenericRepository.ExecuteSQL<StoreImageEntity>("GetStoreImageByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StoreTermsEntity> GetStoreTermsDetail(int Store_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("Store_Id",Store_Id)
            };
                var result = objGenericRepository.ExecuteSQL<StoreTermsEntity>("GetStoreTermsDetail", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductEntity> GetBestSellerListByStoreId(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetBestSellerListByStoreId", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }



    }
}
