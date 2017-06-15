using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICatalogueServices
    {
        List<CatalogueEntity> GetCatalogueList(int StoreId, int LoggedInUserId);

        CatalogueEntity GetCatalogueById(int StoreId, int LoggedInUserId, int CatalogueId);

        int InsertUpdateCatalogue(int StoreId, int LoggedInUserId, CatalogueEntity catalogueEntity);

        int DeleteCatalogue(int catalogue_id, int StoreId, int LoggedInUserId);
    }
}
