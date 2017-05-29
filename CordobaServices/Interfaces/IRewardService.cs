using CordobaModels.Entities;
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
    }
}
