using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IUserDetailBLL : IBaseBLL<UserDetailModel>
    {
        bool checkUseLogin();
        UserDetailModel GetUserByNUID(string Nuid);
        IEnumerable<UserDetailModel> GetUserList();
        int UpdateUserDetail(UserDetailModel userDetailModel);
        UserDetailModel GetUserDetails(int id);
        void DeleteUser(int id, string modified);
        bool CheckDuplicateUserNUID(UserDetailModel userModel);
        bool CheckDuplicateUserEmail(UserDetailModel userDetailModel);
        int UpdateIsActive(int Id, string status);
    }
}
