using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IStoreServices
    {
       List<StoreEntity> GetStoreList(int? StoreID);

       StoreEntity GetStoreById(int store_id);

       int InsertUpdateStore(StoreEntity storeEntity);

       int? DeleteStoreById_Admin(int storeId);
    }
}
