using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces_Layout;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services_Layout
{
   public class CartServices : ICartServices
    {
        private GenericRepository<CartEntity> objGenericRepository = new GenericRepository<CartEntity>();


        public List<CartEntity> GetCartDetailsByCustomerAndStoreId(int? StoreID, int CustomerId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                 , new SqlParameter("CustomerId",CustomerId)
                                               };

            var result = objGenericRepository.ExecuteSQL<CartEntity>("GetCartDetailsByCustomerAndStoreId", sqlParameter).ToList();
            return result;

        }

        public List<CartEntity> GetCartDetailsByCartGroupId(int? StoreID, int cartgroup_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                 , new SqlParameter("cartgroup_id",cartgroup_id)
                                               };

            var result = objGenericRepository.ExecuteSQL<CartEntity>("GetCartDetailsByCartGroupId", sqlParameter).ToList();
            return result;

        }

        public int? RemoveProductFromCart(int? CartId)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("cart_id", CartId)                                             
                                               };

            var result = objGenericRepository.ExecuteSQL<int>("DeleteProductFromCart", sqlParameter).FirstOrDefault();
            return result;

        }

        public int? PlaceOrder(PlaceOrderEntity placeOrderObj)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                     new SqlParameter("store_id", placeOrderObj.store_id)   
                                                   ,  new SqlParameter("customer_id", placeOrderObj.customer_id)  
                                                   ,  new SqlParameter("shipping_addressId", placeOrderObj.shipping_addressId)  
                                                   ,  new SqlParameter("ip", placeOrderObj.IpAddress!=null? placeOrderObj.IpAddress:(object)DBNull.Value)  
                                                   ,  new SqlParameter("Comment", placeOrderObj.Comment!=null?placeOrderObj.Comment:(object)DBNull.Value)  
                                                   ,  new SqlParameter("CartGroupId", placeOrderObj.CartGroupId)  
                                               };

            var result = objGenericRepository.ExecuteSQL<int>("PlaceOrderAfterConfirmation", sqlParameter).FirstOrDefault();
            return result;

        }
    }
}
