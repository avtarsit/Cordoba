using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class ProductEntity
    {
        public int? product_id { get; set; }
        public string name { get; set; }



        public string Model { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string  ImagePath {get;set;}
        public int StatusId { get; set; }
        public string StatusName { get; set; }


        public int? stock_status_id { get; set; }
        public int? manufacturer_id { get; set; }
        public int? shipping { get; set; }
        public DateTime? date_available { get; set; }
        public DateTime? date_added { get; set; }
        public DateTime? date_modified { get; set; }

        public int? sort_order { get; set; }
        public int? subtract { get; set; }
        public int? minimum { get; set; }
        public int? status { get; set; }

        public decimal? cost { get; set; }
        public int? supplier_id { get; set; }

        public List<CatalogueEntity> CatalogueList { get; set; }
        public List<ProductDescriptionList> ProductDescriptionList { get; set; }


    }
}
