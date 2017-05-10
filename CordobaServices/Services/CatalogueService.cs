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
    public class CatalogueService : ICatalogueServices
    {



        private GenericRepository<CatalogueEntity> objGenericRepository = new GenericRepository<CatalogueEntity>();

        public List<CatalogueEntity> GetCatalogueList()
        {
            List<CatalogueEntity> Catalogue = new List<CatalogueEntity>();
            try
            {
                var Result = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetCatalogueList").ToList<CatalogueEntity>();

                if (Result != null)
                    Catalogue = Result.ToList();

            }
            catch (Exception ex)
            {
                //_logger.Error(ex);
            }
            return Catalogue;
        }

        public CatalogueEntity GetCatalogueById(int CatalogueId = 0)
        {
            CatalogueEntity ProductCatalogueEntity = new CatalogueEntity();
            if (CatalogueId > 0)
            {
                ProductCatalogueEntity = (from t in GetCatalogueList()
                                  where t.CatalogueId == CatalogueId
                                  select t).FirstOrDefault();
            }
            else
            {
                ProductCatalogueEntity = new CatalogueEntity();
            }
            return ProductCatalogueEntity;

        }
    }
}
