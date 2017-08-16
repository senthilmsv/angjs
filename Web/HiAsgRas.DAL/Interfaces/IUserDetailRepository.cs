using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IUserDetailRepository : IRepository<UserDetail>
    {
        bool checkUserLogin();
        IEnumerable<UserDetailModel> GetUserList();
        UserDetail GetUserDetails(int Id);
        bool CheckDuplicateUserNUID(UserDetailModel userModel);
        bool CheckDuplicateUserEmail(UserDetailModel userDetailModel);
        int UpdateIsActive(int Id, string status);
    }
}
