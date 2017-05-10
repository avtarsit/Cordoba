using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
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

     

       public ManufacturersEntity GetManufaturerDetail(int? ManufacturersID)
       {
           ManufacturersEntity Manufacturer = new ManufacturersEntity();
           if(ManufacturersID>0)
           {
                Manufacturer = (from t in GetManufacturersList()
                                          where t.ManufacturerID==ManufacturersID
                                          select t).FirstOrDefault();
           }
           else
           {
                Manufacturer = new ManufacturersEntity();
           }        
            ManufacturersStoreEntity ManufacturerStoreList = new ManufacturersStoreEntity();
           List<StoreEntity> StoreList = new List<StoreEntity>();
           StoreList.Add(new StoreEntity() { StoreID = 0, StoreName = "Default"                        ,IsSelected=false });
           StoreList.Add(new StoreEntity() { StoreID = 1, StoreName = "Make a Difference Thank You AE" ,IsSelected=true});
           StoreList.Add(new StoreEntity() { StoreID = 2, StoreName = "Arkle Finance rewards 2015"     , IsSelected =false});
           StoreList.Add(new StoreEntity() { StoreID = 3, StoreName = "Make a Difference Thank You AU" , IsSelected =false});
           StoreList.Add(new StoreEntity() { StoreID = 4, StoreName = "Make a Difference Thank You CA" , IsSelected =true});
           StoreList.Add(new StoreEntity() { StoreID = 5, StoreName = "Make a Difference Thank You FR" , IsSelected= false});
           StoreList.Add(new StoreEntity() { StoreID = 6, StoreName = "Make a Difference Thank You JP" , IsSelected =false});
           StoreList.Add(new StoreEntity() { StoreID = 7, StoreName = "Make a Difference Thank You IN" , IsSelected =true});
           StoreList.Add(new StoreEntity() { StoreID = 8, StoreName = "Make a Difference Thank You NZ" , IsSelected =false});
           StoreList.Add(new StoreEntity() { StoreID = 9, StoreName = "Make a Difference Thank You US" , IsSelected = false });
           StoreList.Add(new StoreEntity() { StoreID = 10,StoreName= "Annodata rewards"              ,IsSelected= false });

           ManufacturerStoreList.ManufacturerID = ManufacturersID;
           ManufacturerStoreList.ManufacturerStore = StoreList;
           Manufacturer.ManufacturerStoreList = ManufacturerStoreList;
           return Manufacturer;

       }


    }
}
