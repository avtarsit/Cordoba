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
        List<CatalogueEntity> GetCatalogueList();

        CatalogueEntity GetCatalogueById(int CategoryId);

        int InsertUpdateCatalogue(CatalogueEntity catalogueEntity);
    }
}
