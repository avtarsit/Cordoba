using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Helpers; 

namespace CordobaServices.Services
{
    public class ReportService : IReportService
    {
        private GenericRepository<ReportEntity> ReportEntityGenericRepository = new GenericRepository<ReportEntity>();

        public IEnumerable<ReportEntity> GetReturnList(string sortColumn, DateTime DateStart, DateTime DateEnd, int GroupById, int StatusId, TableParameter<ReportEntity> filter, string PageFrom = "")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("DateTime", DateStart!=null ? DateStart:(object)DBNull.Value)
                    ,new SqlParameter("DateEnd", DateEnd!=null ? DateEnd:(object)DBNull.Value)
                    ,new SqlParameter("GroupById", GroupById!=null ? GroupById:(object)DBNull.Value)
                    ,new SqlParameter("StatusId", StatusId!=null ? StatusId:(object)DBNull.Value)
                };


                var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("EXEC GetReturnList", param).AsQueryable();

                return query;

                //var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                //var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                //var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                //var paramPageFrom = new SqlParameter { ParameterName = "PageFrom", DbType = DbType.String, Value = PageFrom };
                //var query = ReportEntityGenericRepository.ExecuteSQL<ReportEntity>("GetReturnList", paramOrderBy, paramPageSize, paramPageIndex, paramPageFrom).AsQueryable();
                //return query;
            }
            catch (Exception)
            {
                throw;
            }

            //return result;
        }
    }
}
