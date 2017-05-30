using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class RewardUserDetailsEntity
    {
        public int id { get; set; }
        public int reward_user_id { get; set; }
        public int reward_givenby_userid { get; set; }
        public int reward_value_id { get; set; }
        public DateTime reward_given_date { get; set; }
        public string comment { get; set; }
        public string Medal { get; set;}
        public string Customer { get; set; }
    }
}
