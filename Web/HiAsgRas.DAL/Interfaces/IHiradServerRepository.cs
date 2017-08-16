using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;


namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradServerRepository : IRepository<HiradServer>
    {
        IEnumerable<HiradServerModel> GetServerAll();
        HiradServer GetSeverDetails(int Id);
        List<HiradServerListSearch_Result> searchHiradServerList_Procedure(HiradServerListSearch_Result objEntity);

        IEnumerable<HiradServerModel> GetAllMonitoringServers();
        bool UpdateMonitorInfo(string Ids, string source);

        
        List<AppsListModel> GetAllAppsByServerId(int ServerId);
        bool CheckDuplicateSystemName(HiradServerModel hiradServerModel);
        
    }
}
