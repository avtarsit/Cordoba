﻿using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IRewardService
    {
        List<RewardEntity> GetRewardList(int reward_id = 0);
        List<RewardTypeEntity> GetRewardTypeList();
        int InsertOrUpdateReward(RewardEntity objRewardEntity, int isAddMode);
        int DeleteReward(int reward_id);
        List<RewardUserEntity> ViewCustomerRewards(int reward_id);
        List<RewardUserDetailsEntity> GetRewardCustomerDetails(int reward_user_id);
        int DeleteRewardUser(int id, int reward_user_id);
        List<RewardUserDetailsEntity> MyRewards(int id);
        List<RewardEntity> GetAllRunningRewards();
        List<RewardUserEntity> RewardCustomerDetails(int reward_id);
        List<AddRewardEntity> GetRewardGroupCustomers(int loginUserId,int reward_id);
        int AddCustomer_Reward(AddRewardEntity objAddRewardEntity);
        int Declare_RewardWinner(int reward_id, int reward_user_id, string admin_comment);
        int Delete_RewardWinner(int reward_user_id);
        List<RewardUserEntity> Dashboard_RewardWinner();
    }
}
