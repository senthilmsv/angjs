using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IHiradDbMonitorBLL : IBaseBLL<HiradDbMonitorModel>
    {
        IEnumerable<HiradDbMonitorModel> GetDBServerAll();
        List<HiradDbMonitorModel> HiradDbList_procedure();
        int UpdateDb(HiradDbMonitorModel hiradAppModel);
        HiradDbMonitorModel GetDbDetails(int id);
        void DeleteDb(int id, string modified);
        bool CheckDuplicateDatabase(HiradDbMonitorModel hiradDbMonitorModel);
        List<DbMonitorLogStatusByLastRunModel> GetAllDBLogStatusByLastRun();
        HiradDbMonitorModel GetAllDBMonitorLogsByDB(int id);
    }
}
