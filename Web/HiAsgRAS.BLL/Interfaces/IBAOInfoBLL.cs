using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.ViewModel;
namespace HiAsgRAS.BLL.Interfaces
{
    public interface IBAOInfoBLL:IBaseBLL<BAOInfoModel>
    {
        IEnumerable<BAOInfoModel> GetBAOList();
        int UpdateBAOInfo(BAOInfoModel baoInfoModel);
        BAOInfoModel GetBAODetails(int id);
        void DeleteBAO(int id, string modified);
        List<BAOInfoModel> searchBAOList_procedure(BAOInfoModel baoInfoModel);
        bool CheckDuplicateBAO(BAOInfoModel baoInfoModel);
       // int UpdateIsActive(int Id, string status);
        List<AppsListModel> GetAllAppsByBAOId(int baoId);
        BAOInfoModel GetBaoInfoByApplication(int appId, string appType);
    }
   
}
