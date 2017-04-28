using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class StoreEntity
    {
       public int? StoreID { get; set; }
       public string StoreName { get; set; }
       public bool IsSelected { get; set; }
       public string StoreURL { get; set; }
    }
}
