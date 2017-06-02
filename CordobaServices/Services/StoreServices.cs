using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class StoreServices : IStoreServices
    {
        private GenericRepository<StoreEntity> objGenericRepository = new GenericRepository<StoreEntity>();

       public List<StoreEntity> GetStoreList(int? StoreID)
       {
           List<StoreEntity> StoreList = new List<StoreEntity>();
           var paramStoreId = new SqlParameter { ParameterName = "StoreId", DbType = DbType.Int32, Value = StoreID };
           StoreList = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreList", paramStoreId).ToList();
           return StoreList;
       }


       public StoreEntity GetStoreById(int store_id)
       {
           StoreEntity storeEntity = new StoreEntity();

           var paramStoreId = new SqlParameter { ParameterName = "store_id", DbType = DbType.Int32, Value = store_id };
           var result = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreById", paramStoreId).FirstOrDefault();
           if (result!=null)
           {
               storeEntity = result;
           }
           return storeEntity;
       }
    }
}
