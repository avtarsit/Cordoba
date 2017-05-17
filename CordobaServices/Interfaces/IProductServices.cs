using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IProductServices
    {
        List<ProductEntity> GetProductList();
        ProductEntity GetProductById(int product_id);
        int AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id);
        int DeleteProductFromCart(int cart_id);
    }
}
