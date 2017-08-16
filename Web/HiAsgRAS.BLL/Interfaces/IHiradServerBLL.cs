using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IHiradServerBLL : IBaseBLL<HiradServerModel>
    {
        IEnumerable<HiradServerModel> GetServerAll();
        int UpdateServer(HiradServerModel hiradServerModel);
        HiradServerModel GetServerDetails(int id);
        void DeleteServer(int Id, string modified);
        List<HiradServerModel> searchHiradServerList_procedure(HiradServerModel hiradModel);

        IEnumerable<HiradServerModel> GetAllMonitoringServers();

        SystemInformationModel GetSystemInformation(string systemName);
        bool UpdateMonitorInfo(string Ids, string source);

        void UpdateAllSystemInformation();
        bool CheckDuplicateSystemName(HiradServerModel hiradServerModel);
        List<AppsListModel> GetAllAppsByServerId(int ServerId);

        void UpdateServerStatus(int id, int statusTypeId);
    }
}
