using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IHiradServerLogBLL : IBaseBLL<HiradServerLogModel>
    {
        List<HiradServerLogStatusModel> GetAllServerLogStatus(int? serverId, System.DateTime? fromDate, System.DateTime? toDate);
        List<LogStatusByLastRunModel> GetAllServerLogStatusByLastRun();

        List<GetServerLogStatusByDateModel> GetServerLogStatusByDate(System.DateTime? LoggedAt);

        HiradServerModel GetAllLogsByServer(string serverName, int? serverId);

        List<LogStatusByLastRunModel> GetAllRAMPercentageByLastRun();
        List<GetServerLogStatusByDateModel> GetAllRAMPercentageByDate(System.DateTime? LoggedAt);

        List<LogStatusByLastRunModel> GetAllHDDPercentage();
        List<LogStatusByLastRunModel> GetAllRebootServers();
    }
}
