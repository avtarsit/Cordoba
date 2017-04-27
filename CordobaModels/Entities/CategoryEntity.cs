using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class CategoryEntity
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int?  SortOrder {get;set;}
        public int? CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
