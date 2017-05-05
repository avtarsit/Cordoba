using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class LanguageEntity
    {
       public int? LanguageId { get; set; }
       public string LanguageName { get; set; }
       public string LanguageCode { get; set; }
       public string Local { get; set; }
       public string ImageName { get; set; }
       public string Directory { get; set; }
       public string StatusCd { get; set; }       
    }
}
