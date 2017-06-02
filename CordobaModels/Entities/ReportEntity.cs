using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class ReportEntity
    {
       public int? GroupById { get; set; }
       public int? StatusId { get; set; }
       public Nullable<DateTime> DateStart { get; set; }
       public Nullable<DateTime> DateEnd { get; set; }
       public int? No_Of_Orders { get; set; }
       public int TotalRecords { get; set; }
    }
}
