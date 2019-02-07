using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class BestSellerEntity
    {
        public int product_id { get; set; }
        public string model { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public int StoreId { get; set; }
        public int? bestseller_Id { get; set; }
        public int TotalRecords { get; set; }
    }
}
