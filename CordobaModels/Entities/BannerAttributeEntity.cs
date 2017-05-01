using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class BannerAttributeEntity
    {
        int Id { get; set; }
        string Title { get; set; }
        string Link { get; set; }
        string Image { get; set; }
        Nullable<int> SortOrder { get; set; }
    }
}

