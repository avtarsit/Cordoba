using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface ISupplierServices
    {
        List<SupplierEntity> GetSupplierList(int? SupplierID);
        SupplierEntity GetSupplierDetail(int? SupplierID);
        int InsertOrUpdateSupplier(SupplierEntity objSupplier);
        int DeleteSupplier(int supplierId);
    }
}
