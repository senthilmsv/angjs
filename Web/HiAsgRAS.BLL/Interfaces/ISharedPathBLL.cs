using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface ISharedPathBLL : IBaseBLL<SharedPathModel>
    {
        int UpdateSharedPathList(SharedPathModel sharedPathModel);
        List<SharedPathModel> GetSharedNWPathDetails();
        SharedPathModel GetSharedNWPathDetailsById(int id);
        void DeleteSharedPath(int id, string modified);
    }
}
