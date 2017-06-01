
using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class UserServices : IUserServices
    {
        private GenericRepository<UserEntity> UserEntityGenericRepository = new GenericRepository<UserEntity>();
        public List<UserEntity> GetUserList()
        {
            try
            {
                var UserList = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetUserList").ToList<UserEntity>().ToList();
                return UserList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity GetUserDetail(int userID = 0)
        {
            UserEntity UserDetail = new UserEntity();
            if (userID > 0)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("user_id", userID);
                    UserDetail = UserEntityGenericRepository.ExecuteSQL<UserEntity>("EXEC GetUserDetail", param).ToList<UserEntity>().FirstOrDefault();

                }
                catch (Exception)
                {

                    throw;
                }

            }

            return UserDetail;

        }


        public int CreateOrUpdateUser(int LoggedInUserId, UserEntity user)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[10];
                param[0] = new SqlParameter("user_id", user.user_id);
                param[1] = new SqlParameter("user_group_id", user.user_group_id);
                param[2] = new SqlParameter("username", user.username);
                param[3] = new SqlParameter("password", user.password);
                param[4] = new SqlParameter("firstname", user.firstname);
                param[5] = new SqlParameter("lastname", user.lastname);
                param[6] = new SqlParameter("email", user.email != null ? user.email : (object)DBNull.Value);
                param[7] = new SqlParameter("status", user.status);
                param[8] = new SqlParameter("ip", user.ip != null ? user.ip : (object)DBNull.Value);
                param[9] = new SqlParameter("image", user.image != null ? user.image : (object)DBNull.Value);

                var result = UserEntityGenericRepository.ExecuteSQL<int>("EXEC InsertOrUpdateUser", param).ToList<int>().FirstOrDefault();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public int DeleteUserDetail(int LoggedInUserId, int UserId)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("LoggedInUserId", LoggedInUserId);
                param[1] = new SqlParameter("user_id", UserId);
                var Result = UserEntityGenericRepository.ExecuteSQL<int>("EXEC DeleteUser @LoggedInUserId,@user_id", param).ToList<int>().FirstOrDefault();
                return Result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
