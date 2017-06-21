using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IProductServices
    {
        List<ProductEntity> GetProductList(int StoreId, int LoggedInUserId, string sortColumn, TableParameter<ProductEntity> filter,  string name, decimal? Price, int? status, string Model, int? Quantity);
        ProductEntity GetProductById(int StoreId, int LoggedInUserId, int product_id);
        CartEntity AddProductToCart(int store_id, int LoggedInUserId, int customer_id, int product_id, int qty, int cartgroup_id);
        int DeleteProductFromCart(int StoreId, int LoggedInUserId, int cart_id);

        int InsertUpdateProduct(int StoreId, int LoggedInUserId, ProductEntity productEntity);
        List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int LoggedInUserId, int CategoryId, int Customer_Id,string WhatAreYouLookingFor);

        int DeleteProduct(int StoreId, int LoggedInUserId, int product_id);
        ProductEntity GetProductDetailForLayout(int StoreId, int LoggedInUserId, int ProductId);
        List<ProductEntity> GetRelatedProductList(int StoreId, int LoggedInUserId, int SelectedProductId, int RelatedProductId);

        //InsertAsHotProduct
        int InsertAsHotProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity);
        int InsertAsSpecialProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity);
        List<HotSpecialProductEntity> GetHotOrSpecialProductById(int language_id, int store_id, int LoggedInUserId, int product_id);
        List<HotSpecialProductEntity> GetHotOrSpecialProductDetailById(int store_id, int LoggedInUserId, bool IsHotProduct, int product_id);
    }
}
