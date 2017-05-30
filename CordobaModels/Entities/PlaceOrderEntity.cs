using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class PlaceOrderEntity
    {
       public int? store_id { get; set; }
       public int? customer_id { get; set; }
       public int? shipping_addressId { get; set; }
       public string IpAddress { get; set; }
       public string Comment { get; set; }
       public int? CartGroupId { get; set; }

    }
}
