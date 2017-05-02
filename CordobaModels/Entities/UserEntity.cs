using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
   public class UserEntity
    {
       public int? UserID { get; set; }
       public string UserName { get; set; }
       public int? UserGroupID { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Email { get; set; }
       public byte[] Image { get; set; }
       public string Password { get; set; }
       public string StatusCd { get; set; }
       public string StatusName { get; set; }
       public Nullable<DateTime> CreatedDate { get; set; }
    }
}
