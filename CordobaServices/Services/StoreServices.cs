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
            if (result != null)
            {
                storeEntity = result;
            }
            return storeEntity;
        }


        public int InsertUpdateStore(StoreEntity storeEntity)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id", storeEntity.store_id)
                                                 , new SqlParameter("url", storeEntity.url?? (object) DBNull.Value)
                                                 , new SqlParameter("name", storeEntity.name ??  DBNull.Value.ToString())
                                                 , new SqlParameter("title", storeEntity.title ??  DBNull.Value.ToString())
                                                 , new SqlParameter("description", storeEntity.description ??  DBNull.Value.ToString())
                                                 , new SqlParameter("terms", storeEntity.terms ??   DBNull.Value.ToString())

                                                 , new SqlParameter("address", storeEntity.address ??   DBNull.Value.ToString())
                                                 , new SqlParameter("email", storeEntity.email ??   DBNull.Value.ToString())
                                                 , new SqlParameter("telephone", storeEntity.telephone ??   DBNull.Value.ToString())
                                                 , new SqlParameter("logo", storeEntity.logo ??   DBNull.Value.ToString())

                                                 , new SqlParameter("meta_title", storeEntity.meta_title ??   DBNull.Value.ToString())
                                                 , new SqlParameter("meta_description", storeEntity.meta_description ??   DBNull.Value.ToString())
                                                 , new SqlParameter("css_overrides", storeEntity.css_overrides ??   DBNull.Value.ToString())
                                                 , new SqlParameter("template", storeEntity.template ??   DBNull.Value.ToString())
                                                 , new SqlParameter("layout", storeEntity.layout ??   DBNull.Value.ToString())
                                                 
                                                 , new SqlParameter("country_id", storeEntity.country_id)
                                                 , new SqlParameter("language", storeEntity.language  ??   DBNull.Value.ToString())
                                                 , new SqlParameter("currency", storeEntity.currency  ??   DBNull.Value.ToString())
                                                 , new SqlParameter("county", storeEntity.county      ??    DBNull.Value.ToString())
                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertUpdateStore", sqlParameter).FirstOrDefault();
            return result;

        }
    }
}
