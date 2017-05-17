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
           //StoreList.Add(new StoreEntity() { StoreID = 1, StoreName = "Main (Default)", StoreURL = "	http://webapp2.cordobarewards.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 2, StoreName = "Make a Difference Thank You AE", StoreURL = "http://ae.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 3, StoreName = "Arkle Finance rewards 2015", StoreURL = "http://ae.pbmakeadifferencethankyou.com/" });
           //StoreList.Add(new StoreEntity() { StoreID = 4, StoreName = "Make a Difference Thank You AU", StoreURL = "http://arkle2016.cordobarewards.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 5, StoreName = "Make a Difference Thank You CA", StoreURL = "http://ca.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 6, StoreName = "Make a Difference Thank You FR", StoreURL = "http://fr.pbmakeadifferencethankyou.com/" });
           //StoreList.Add(new StoreEntity() { StoreID = 7, StoreName = "Make a Difference Thank You IN", StoreURL = "	http://in.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 3, StoreName = "Make a Difference Thank You JP", StoreURL = "http://jp.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 4, StoreName = "Make a Difference Thank You NZ", StoreURL = "http://nz.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 5, StoreName = "Make a Difference Thank You US", StoreURL = "http://us.pbmakeadifferencethankyou.com/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 6, StoreName = "Example", StoreURL = " http://webapp2.cordobarewards.co.uk/" });
           //StoreList.Add(new StoreEntity() { StoreID = 7, StoreName = "Annodata Rewards", StoreURL = "http://www.annodatarewards.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 3, StoreName = "Arkle Finance rewards", StoreURL = "http://www.arkleloyalty2017.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 4, StoreName = "Asset Advantage Store", StoreURL = " http://www.assetadvantagerewards.co.uk/" });
           //StoreList.Add(new StoreEntity() { StoreID = 5, StoreName = "Blizzard Rewards", StoreURL = "http://www.blizzardrewards.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 6, StoreName = "BT Local Business Rewards", StoreURL = "http://www.btlblondonrewards.co.uk/ " });
           //StoreList.Add(new StoreEntity() { StoreID = 7, StoreName = "	Clear Asset Finance", StoreURL = " 	http://www.clearwinnersclub.co.uk/" });
           return StoreList;
       }
    }
}
