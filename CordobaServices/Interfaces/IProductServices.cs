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
        IEnumerable<ProductEntity> GetProductList(string sortColumn, TableParameter<ProductEntity> filter, string PageFrom = "");
        ProductEntity GetProductById(int product_id);
        int AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id);
        int DeleteProductFromCart(int cart_id);

        int InsertUpdateProduct(ProductEntity productEntity);
        List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int CategoryId);
    }
}
