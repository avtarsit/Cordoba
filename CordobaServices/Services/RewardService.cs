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
    public class RewardService : IRewardService
    {
        private GenericRepository<RewardEntity> objGenericRepository = new GenericRepository<RewardEntity>();

        public List<RewardEntity> GetRewardList(int reward_id = 0)
        {
            List<RewardEntity> listOfRewards = new List<RewardEntity>();
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            listOfRewards = objGenericRepository.ExecuteSQL<RewardEntity>("GetRewardList", paramRewardId).ToList();
            return listOfRewards;
        }

        public List<RewardTypeEntity> GetRewardTypeList()
        {
            List<RewardTypeEntity> listOfRewardtype = new List<RewardTypeEntity>();
            listOfRewardtype = objGenericRepository.ExecuteSQL<RewardTypeEntity>("GetRewardTypeList").ToList();
            return listOfRewardtype;
        }

        public int InsertOrUpdateReward(RewardEntity objRewardEntity, int isAddMode)
        {
            var paramRewardId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = objRewardEntity.id };
            var paramRewardTitle = new SqlParameter { ParameterName = "Title", DbType = DbType.String, Value = objRewardEntity.Title };
            var paramreward_type_id = new SqlParameter { ParameterName = "reward_type_id", DbType = DbType.Int32, Value = objRewardEntity.reward_type_id };
            var paramstart_date = new SqlParameter { ParameterName = "start_date", DbType = DbType.DateTime, Value = objRewardEntity.start_date };
            var paramend_date = new SqlParameter { ParameterName = "end_date", DbType = DbType.DateTime, Value = objRewardEntity.end_date };
            var paramstatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = objRewardEntity.status };
            var paramdescription = new SqlParameter { ParameterName = "description", DbType = DbType.String, Value = objRewardEntity.description ?? (object)DBNull.Value };
            var paramsent_mail = new SqlParameter { ParameterName = "sent_mail", DbType = DbType.Boolean, Value = objRewardEntity.sent_mail };
            var paramdisplay_dashborad = new SqlParameter { ParameterName = "display_dashborad", DbType = DbType.Boolean, Value = objRewardEntity.display_dashborad };
            var paramcreated_by = new SqlParameter { ParameterName = "created_by", DbType = DbType.Int32, Value = objRewardEntity.created_by };
            var paramisAddMode = new SqlParameter { ParameterName = "isAddMode", DbType = DbType.Int32, Value = isAddMode };
            var parammodified_by = new SqlParameter { ParameterName = "modified_by", DbType = DbType.Int32, Value = objRewardEntity.modified_by ?? (object)DBNull.Value };
            int rewardId = objGenericRepository.ExecuteSQL<int>("InsertOrUpdateReward", paramRewardId, paramRewardTitle, paramreward_type_id, paramstart_date, paramend_date,
                             paramstatus, paramdescription, paramsent_mail, paramdisplay_dashborad, paramcreated_by, paramisAddMode, parammodified_by).SingleOrDefault();
            return rewardId;
        }

        public int DeleteReward(int reward_id)
        {
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            int result = objGenericRepository.ExecuteSQL<int>("DeleteReward", paramRewardId).SingleOrDefault();
            return result;
        }

        public List<RewardUserEntity> ViewCustomerRewards(int reward_id)
        {
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var rewardCustomerlist = objGenericRepository.ExecuteSQL<RewardUserEntity>("ViewCustomer_Reward", paramRewardId).ToList();
            return rewardCustomerlist;
        }

        public List<RewardUserDetailsEntity> GetRewardCustomerDetails(int reward_user_id)
        {
            var paramrewardUserId = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            var rewardCustomerDetails = objGenericRepository.ExecuteSQL<RewardUserDetailsEntity>("GetReward_CustomerDetails", paramrewardUserId).ToList();
            return rewardCustomerDetails;
        }

        public int DeleteRewardUser(int id, int reward_user_id)
        {
            var paramId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = id };
            var paramRewardUserId = new SqlParameter { ParameterName = "reward_user_id", DbType = DbType.Int32, Value = reward_user_id };
            var deletedRecord = objGenericRepository.ExecuteSQL<int>("Delete_RewardUser", paramId, paramRewardUserId).SingleOrDefault();
            return deletedRecord;
        }

        public List<RewardUserDetailsEntity> MyRewards(int id)
        {
            var paramId = new SqlParameter { ParameterName = "id", DbType = DbType.Int32, Value = id };
            var myRewardlist = objGenericRepository.ExecuteSQL<RewardUserDetailsEntity>("GetMyRewards", paramId).ToList();
            return myRewardlist;
        }

        public List<RewardEntity> GetAllRunningRewards()
        {
            var runningAwards = objGenericRepository.ExecuteSQL<RewardEntity>("GetAllRunningRewards").ToList();
            return runningAwards;
        }

        public List<RewardUserEntity> RewardCustomerDetails(int reward_id)
        {
            var paramReawrdId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var rewardcustomers = objGenericRepository.ExecuteSQL<RewardUserEntity>("RewardCustomerDetails", paramReawrdId).ToList();
            return rewardcustomers;
        }

        public List<AddRewardEntity> GetRewardGroupCustomers(int loginUserId,int reward_id)
        {
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = reward_id };
            var paramLoginUserId = new SqlParameter { ParameterName = "loginUserid", DbType = DbType.Int32, Value = loginUserId };
            var rewardGroupCustomers = objGenericRepository.ExecuteSQL<AddRewardEntity>("GetReward_GroupCustomers", paramRewardId, paramLoginUserId).ToList();
            return rewardGroupCustomers;
        }

        public int AddCustomer_Reward(AddRewardEntity objAddRewardEntity)
        {
            if (objAddRewardEntity.reward_type_id == Convert.ToInt32(SystemEnum.RewardType.Star))
            {
                objAddRewardEntity.reward_name = "Star " + Convert.ToString(objAddRewardEntity.NoOfStars);
            }
            var paramRewardId = new SqlParameter { ParameterName = "reward_id", DbType = DbType.Int32, Value = objAddRewardEntity.reward_id };
            var paramISWinner = new SqlParameter { ParameterName = "IsWinner", DbType = DbType.Boolean, Value = false };
            var paramCustomerId = new SqlParameter { ParameterName = "reward_giventouser_id", DbType = DbType.Int32, Value = objAddRewardEntity.customer_id };
            var paramLoginUserId = new SqlParameter { ParameterName = "reward_givenby_userid", DbType = DbType.Int32, Value = objAddRewardEntity.loginUserid };
            var paramRewardName = new SqlParameter { ParameterName = "reward_name", DbType = DbType.String, Value = objAddRewardEntity.reward_name };
            var paramRewardTypeId = new SqlParameter { ParameterName = "reward_type_id", DbType = DbType.Int32, Value = objAddRewardEntity.reward_type_id };
            var paramComment = new SqlParameter { ParameterName = "comment", DbType = DbType.String, Value = objAddRewardEntity.Comment ?? (object)DBNull.Value };
            int reward_inserted_id = objGenericRepository.ExecuteSQL<int>("AddCustomer_Reward", paramRewardId, paramISWinner, paramCustomerId, paramLoginUserId,
                paramRewardName, paramRewardTypeId, paramComment).SingleOrDefault();
            return reward_inserted_id;
        }
    }
}
