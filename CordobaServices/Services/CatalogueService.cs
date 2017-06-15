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
    public class CatalogueService : ICatalogueServices
    {



        private GenericRepository<CatalogueEntity> objGenericRepository = new GenericRepository<CatalogueEntity>();

        public List<CatalogueEntity> GetCatalogueList(int StoreId, int LoggedInUserId)
        {
            List<CatalogueEntity> Catalogue = new List<CatalogueEntity>();
            try
            {
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var Result = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetCatalogueList", paramStoreId, paramLoggedInUserId).ToList<CatalogueEntity>();

                if (Result != null)
                    Catalogue = Result.ToList();

            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return Catalogue;
        }

        public CatalogueEntity GetCatalogueById(int StoreId, int LoggedInUserId,int CatalogueId = 0)
        {
            CatalogueEntity ProductCatalogueEntity = new CatalogueEntity();
            if (CatalogueId > 0)
            {
                var paramCatalogueId = new SqlParameter
                {
                    ParameterName = "CatalogueId",
                    DbType = DbType.Int32,
                    Value = CatalogueId
                };
                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var result = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetCatalogueById", paramCatalogueId,paramStoreId,paramLoggedInUserId).FirstOrDefault();
                ProductCatalogueEntity = result;
            }
            else
            {
                ProductCatalogueEntity = new CatalogueEntity();
            }
             
            return ProductCatalogueEntity;

        }


        public int InsertUpdateCatalogue(int StoreId, int LoggedInUserId,CatalogueEntity catalogueEntity)
        {
            var catalogueIdparam = new SqlParameter 
            { 
                ParameterName = "catalogue_Id", 
                DbType = DbType.Int32, 
                Value = catalogueEntity.catalogue_Id 
            };   
            
            var nameparam = new SqlParameter 
            { 
                ParameterName = "Name", 
                DbType = DbType.String, 
                Value = catalogueEntity.Name 
            };

            var paramStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };

            var paramLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserIdId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var result = objGenericRepository.ExecuteSQL<int>("InsertUpdateCatalogue",catalogueIdparam, nameparam, paramStoreId, paramLoggedInUserId ).FirstOrDefault();
            return result;
        }


        public int DeleteCatalogue(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var paramCatalogueId = new SqlParameter 
                { 
                    ParameterName = "catalogue_id", 
                    DbType = DbType.Int32, 
                    Value = catalogue_id 
                };

                var paramStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };

                var paramLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserIdId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };

                int result = objGenericRepository.ExecuteSQL<int>("DeleteCatalogue", paramCatalogueId, paramStoreId, paramLoggedInUserId).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
         
        }
    }
}
