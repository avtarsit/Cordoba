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
namespace CordobaServices.Services
{
    public class ActivityService : IActivityService
    {
        private GenericRepository<ActivityEntity> ActivityEntityGenericRepository = new GenericRepository<ActivityEntity>();
        
        public List<ActivityEntity> GetActivityList()
        {
            try
            {
                var activityEntity = ActivityEntityGenericRepository.ExecuteSQL<ActivityEntity>("GetActivityList").ToList<ActivityEntity>().ToList();
                return activityEntity;
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
