using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CordobaModels.Entities
{
    public class ProductDescriptionList
    {
        int product_id { get; set; }
        int language_id { get; set; }
        string name { get; set; }
        string meta_keywords { get; set; }
        string meta_description { get; set; }
        string description { get; set; }

    }
}
