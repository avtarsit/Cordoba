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
        //Return Report
        //IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, TableParameter<ReportEntity> filter, string PageFrom = "");

        IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");
        //Sales order Report
        IEnumerable<ReportEntity> GetOrderReportList(string sortColumn, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");
        
        //Transaction Report
        IEnumerable<ReportEntity> GetTransactionReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");

        //Transaction Item Report
        IEnumerable<ReportEntity> GetTransactionItemReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");

        //Transaction Item Category Report
        IEnumerable<ReportEntity> GetTransactionItemCategoryReportList(string sortColumn, DateTime? DateStart, DateTime? DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> filter, string PageFrom = "");
    }
}
