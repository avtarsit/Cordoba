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
    public class ManufacturerServices : IManufacturerServices
    {

        private GenericRepository<ManufacturersEntity> objGenericRepository = new GenericRepository<ManufacturersEntity>();

        public List<ManufacturersEntity> GetManufacturersList()
        {
            List<ManufacturersEntity> ManufacturersList = new List<ManufacturersEntity>();
            try
            {
                var Result = objGenericRepository.ExecuteSQL<ManufacturersEntity>("GetManufacturersList").ToList<ManufacturersEntity>();

                if (Result != null)
                    ManufacturersList = Result.ToList();

            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return ManufacturersList;
        }



        public ManufacturersEntity GetManufaturerDetail(int manufacturer_id)
       {
           ManufacturersEntity Manufacturer = new ManufacturersEntity();
           List<StoreEntity> StoreList = new List<StoreEntity>();
           ManufacturersStoreEntity ManufacturerStoreList = new ManufacturersStoreEntity();
          
           if (manufacturer_id > 0)
           {
               var parammanufacturer_id = new SqlParameter
               {
                   ParameterName = "manufacturer_id",
                   DbType = DbType.Int32,
                   Value = manufacturer_id
               };
               var Result = objGenericRepository.ExecuteSQL<ManufacturersEntity>("GetManufaturerDetail", parammanufacturer_id).FirstOrDefault();
               if (Result != null)
                   Manufacturer = Result;
           }
           else
           {
                Manufacturer = new ManufacturersEntity();
           }
           var parammanufacturerIdForStore = new SqlParameter
           {
               ParameterName = "manufacturer_id",
               DbType = DbType.Int32,
               Value = manufacturer_id
           };
           var storeResult = objGenericRepository.ExecuteSQL<StoreEntity>("GetManufacturerStoreList", parammanufacturerIdForStore).ToList<StoreEntity>();
           if (storeResult != null)
               StoreList = storeResult.ToList();

           ManufacturerStoreList.manufacturer_id = manufacturer_id;
           ManufacturerStoreList.ManufacturerStore = StoreList;
           Manufacturer.ManufacturerStoreList = ManufacturerStoreList;
           return Manufacturer;

       }


    }
}
