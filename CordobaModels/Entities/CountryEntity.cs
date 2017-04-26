using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class CountryEntity
    {
        public int? CountryId { get; set; }
        public string CountryCd { get; set; }
        public string CountryName { get; set; }
        public int? CreatedBy { get; set; }
    }
}
