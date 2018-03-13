using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class StoreHTMLEntity
    {
        public List<StoreSummary> storeSummary { get; set; }
    }

    public class StoreSummary
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
}
