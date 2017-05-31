using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IProductPurchasedReportServices
    {
       List<OrderStatusEntity> GetOrderStatus(int language_id);

       List<OrderProductEntity> GetProductPurchasedList(string sortColumn, int order_status_id, int store_id, Helpers.TableParameter<OrderProductEntity> tableParameter, DateTime? DateStart, DateTime? DateEnd);
    }
}
