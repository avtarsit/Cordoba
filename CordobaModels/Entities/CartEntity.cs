using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class CartEntity
    {
       public int? cart_id { get; set; }
       public int? store_id { get; set; }
       public int? customer_id { get; set; }
       public int? product_id { get; set; }
       public string name { get; set;}
       public string model { get; set; }
       public int? quantity { get; set; }
       public int? cartgroup_id { get; set; }
       public decimal? price { get; set; }
       public decimal? total { get; set; }
       public decimal? tax { get; set; }
       public Nullable<DateTime> createddate { get; set; }
       public int? createdby { get; set; }
       public Nullable<DateTime> updateddate { get; set; }
       public int? updatedby { get; set; }

       public int? TotalItemAdded { get; set; }
       
    }
}
