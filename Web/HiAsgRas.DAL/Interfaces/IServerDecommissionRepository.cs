using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;


namespace HiAsgRAS.DAL.Interfaces
{
  public interface IServerDecommissionRepository : IRepository<HiradServer>
    {
      IEnumerable<HiradServerModel> GetAllServers();
      IEnumerable<HiradAppModel> GetAllClientAppsList(int Id);
      IEnumerable<HiradWebModel> GetAllWebList(int Id);
      IEnumerable<HiradDbMonitorModel> GetDBServerList(int dbserverId);
      bool SaveServerInfo(string Ids, string source, int newServerId, int oldServerId);
    }
}
