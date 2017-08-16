using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IBAOInfoRepository : IRepository<BAOInfo>
    {
        IEnumerable<BAOInfoModel> GetBAOList();
        BAOInfo GetBAODetails(int Id);
        List<BAOSearch_Result> searchBAOSList_Procedure(BAOSearch_Result objEntity);
        bool CheckDuplicateBAO(BAOInfoModel baoInfoModel);
       // int UpdateIsActive(int Id, string status);
        List<AppsListModel> GetAllAppsByBAOId(int baoId);
        BAOInfoModel GetBaoInfoByApplication(int appId,string appType);
    }
}
