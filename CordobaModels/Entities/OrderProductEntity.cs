using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class OrderProductEntity
    {
        public string name { get; set; }
        public int product_id { get; set; }
        public string model { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
        public decimal MainTotal { get; set; }
    }
}
