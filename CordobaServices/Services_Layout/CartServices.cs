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
    }
}
