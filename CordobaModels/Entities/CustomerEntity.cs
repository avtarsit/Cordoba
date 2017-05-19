using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CustomerEntity
    {
        public int customer_id { get; set; }
        public int store_id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public int points { get; set; }
        public string password { get; set; }
        public string cart { get; set; }
        public int newsletter { get; set; }
        public int address_id { get; set; }
        public int status { get; set; }
        public int approved { get; set; }   
        public int activated { get; set; }
        public int customer_group_id { get; set; }
        public int customer_department_id { get; set; }
        public string ip { get; set; }
        public DateTime date_added { get; set; }

        public string WholeName { get; set; }
        public int is_admin { get; set; }

        public string customerName { get; set; }
        public string StatusName { get; set; }
        public string CreatedDate { get; set; }
    }
}
        

