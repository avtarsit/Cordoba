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
        public string name { get; set; }
        public int?  SortOrder {get;set;}
        public int? CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public bool? IsActive { get; set; }
        public CategoryStoreEntity CategoryStoreList { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ParentCategoryName { get; set; }
        public int? StatusId { get; set; }

    }
}
