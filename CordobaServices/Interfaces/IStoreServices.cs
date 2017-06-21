﻿using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IStoreServices
    {
       List<StoreEntity> GetStoreList(int? StoreID, int LoggedInUserId);

       StoreEntity GetStoreById(int store_id, int LoggedInUserId);

       int InsertUpdateStore(StoreEntity storeEntity, int LoggedInUserId);

       int? DeleteStoreById_Admin(int storeId, int LoggedInUserId);
      

       bool UploadStoreImage(int Store_Id, string ImageName, int ImageKey);
    }
}
