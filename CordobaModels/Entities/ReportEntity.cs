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

       public int? No_Of_Products { get; set; }
       public decimal? Total { get; set; }
       public int? Tax { get; set; }


       //Transaction
       public string firstname { get; set; }
       public string lastname { get; set; }
       public string email { get; set; }
       public string store { get; set; }
       public int adjustment { get; set; }
       public int points { get; set; }
       public string comments { get; set; }
       public DateTime Date { get; set; }
       public string type_of_points { get; set; }


    }
}
