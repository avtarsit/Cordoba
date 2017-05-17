using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class StoreEntity
    {
       public int store_id { get; set; }
       public string name { get; set; }
       public bool IsSelected { get; set; }
       public string url { get; set; }
    }
}
