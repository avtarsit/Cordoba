using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class BannerEntity
    {

        public int? BannerId { get; set; }
        public string BannerName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public List<BannerAttributeEntity> BannerAttributeList {get;set;}
    }
}
