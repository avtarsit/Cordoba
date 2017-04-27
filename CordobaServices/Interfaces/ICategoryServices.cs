using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ICategoryServices
    {
        List<CategoryEntity> GetCategoryList(int CategoryId = 0);
    }
}
