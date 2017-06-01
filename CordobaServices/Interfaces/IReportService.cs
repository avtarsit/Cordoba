using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers;

namespace CordobaServices.Interfaces
{
    public interface IReportService
    {
        IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime DateStart, DateTime DateEnd, int GroupById, int StatusId, TableParameter<ReportEntity> filter, string PageFrom = "");
    }
}
