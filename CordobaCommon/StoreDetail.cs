using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaCommon
{
   public class StoreDetail
    {
       
            public int store_id { get; set; }
            public string name { get; set; }
            public bool? IsSelected { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public string meta_description { get; set; }
            public string template { get; set; }
            public int? country_id { get; set; }
            public string language { get; set; }
            public string currency { get; set; }
            public int? tax { get; set; }
            public int? customer_group_id { get; set; }
            public int? customer_price { get; set; }
            public int? customer_approval { get; set; }
            public int? allow_registration { get; set; }
            public int? require_login { get; set; }
            public int? multilingual { get; set; }
            public int? account_id { get; set; }
            public int? checkout_id { get; set; }
            public int? stock_display { get; set; }
            public int? stock_check { get; set; }
            public int? stock_checkout { get; set; }
            public int? order_status_id { get; set; }
            public string logo { get; set; }
            public string icon { get; set; }
            public int? catalog_limit { get; set; }
            public string css_overrides { get; set; }
            public int? language_id { get; set; }
            public string description { get; set; }
        
    }
}
