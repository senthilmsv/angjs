using HiAsgRAS.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradServerLogRepository : IRepository<HiradServerLog>
    {
        IEnumerable<HiradServerLogStatus_Result> GetAllServerLogStatus(int? serverId, System.DateTime? fromDate, System.DateTime? toDate);
        List<GetAllServerLogStatusByLastRun_Result> GetAllServerLogStatusByLastRun();
        List<GetServerLogStatusByDate_Result> GetServerLogStatusByDate(System.DateTime? LoggedAt);
    }
}
