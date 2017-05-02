using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class UserServices : IUserServices
    {
      public List<UserEntity> GetUserList()
       {
           List<UserEntity> UserList = new List<UserEntity>();
           UserList.Add(new UserEntity {UserID=1 ,UserName = "admin", StatusName = "Enable", CreatedDate =Convert.ToDateTime(DateTime.Now.ToShortDateString()) });
           UserList.Add(new UserEntity {UserID=2  ,UserName = "clientadmin", StatusName = "Enable", CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()) });
           UserList.Add(new UserEntity {UserID=3, UserName = "test", StatusName = "Enable", CreatedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()) });
           return UserList;
       }

       public UserEntity GetUserDetail(int userID=0)
      {
          UserEntity UserDetail = new UserEntity();
          if (userID > 0)
           {
               UserDetail = (from t in GetUserList()
                             where t.UserID == userID
                             select t).FirstOrDefault();

           }

          return UserDetail;

      }
    }
}
