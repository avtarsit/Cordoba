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
        List<ManufacturersEntity> GetManufacturersList(int? ManufacturersID);

    }
}
