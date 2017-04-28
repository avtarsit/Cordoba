using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
  public  class ManufacturersEntity
    {
      public int? ManufacturerID { get; set; }
      public string ManufacturerName { get; set; }
      public byte[] image { get;set;}
      public string Description { get; set; }
      
    }
}
