using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradDbMonitorRepository : IRepository<HiradDbMonitor>
    {
        IEnumerable<HiradDbMonitorModel> GetDBServerAll();
        List<HiradDbList_Result> HiradDbList_Procedure();
        GetDBDetailsById_Result GetDbDetails(int Id);
        bool CheckDuplicateDatabase(HiradDbMonitorModel hiradDbMonitorModel);
    }
}
