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
        List<ProductEntity> GetProductList(string sortColumn, TableParameter<ProductEntity> filter,  string name, decimal? Price, int? status, string Model, int? Quantity);
        ProductEntity GetProductById(int product_id);
        int AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id);
        int DeleteProductFromCart(int cart_id);

        int InsertUpdateProduct(ProductEntity productEntity);
        List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int CategoryId);

        int DeleteProduct(int product_id);
    }
}
