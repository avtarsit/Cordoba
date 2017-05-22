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
        public string model { get; set; }
        public string sku { get; set; }
        public string location { get; set; }
        public decimal? price { get; set; }
        public int? quantity { get; set; }
        public string image { get; set; }      
        public string StatusName { get; set; }
        public int? tax_class_id { get; set; }

        public int? stock_status_id { get; set; }
        public int? manufacturer_id { get; set; }
        public int? shipping { get; set; }
        public DateTime? date_available { get; set; }
        public DateTime? date_added { get; set; }
        public DateTime? date_modified { get; set; }
        public decimal? weight { get; set; }    
        public decimal? length { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public int? weight_class_id { get; set; }
        public int? length_class_id { get; set; }
        public int? sort_order { get; set; }
        public int? subtract { get; set; }
        public int? minimum { get; set; }
        public int? status { get; set; }
        public int? viewed { get; set; }
        public decimal? cost { get; set; }
        public int? supplier_id { get; set; }

        public List<CatalogueEntity> CatalogueList { get; set; }
        public List<ProductDescriptionList> ProductDescriptionList { get; set; }


    }
}
