using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ProductEntity
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }

        public string  ImagePath {get;set;}

        public int StatusId { get; set; }
        public string StatusName { get; set; }
      

    }
}
