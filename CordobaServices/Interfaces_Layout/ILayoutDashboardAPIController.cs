using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces_Layout
{
    public interface ILayoutDashboardServices
    {
        List<CategoryEntity> GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory);
    }
}
