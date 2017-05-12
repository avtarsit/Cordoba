using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IManufacturerServices
    {
        List<ManufacturersEntity> GetManufacturersList();
         ManufacturersEntity GetManufaturerDetail(int ManufacturersID);

         int InsertUpdateManufacture(ManufacturersEntity manufacturersEntity);

         int DeleteManufacturer(int manufacturer_id);
    }
}
