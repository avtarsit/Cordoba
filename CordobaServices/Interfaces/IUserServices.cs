 using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
   public interface IUserServices
    {
       List<UserEntity> GetUserList();

       UserEntity GetUserDetail(int UserID = 0);

       int CreateOrUpdateUser(int LoggedInUserId, UserEntity user);
       int DeleteUserDetail(int LoggedInUserId,int UserId);
       bool IsAuthenticUser(UserEntity model);
    }
}
