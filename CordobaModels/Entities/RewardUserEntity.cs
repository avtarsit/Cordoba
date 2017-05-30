using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class RewardUserEntity
    {
        public int reward_user_id { get; set; }
        public int reward_id { get; set; }
        public int reward_giventouser_id { get; set; }
        public bool IsWinner { get; set; }
        public string Rewards { get; set; }
        public string Customer { get; set; }
    }
}
