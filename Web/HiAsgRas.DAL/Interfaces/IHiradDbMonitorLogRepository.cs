using HiAsgRAS.DAL.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradDbMonitorLogRepository : IRepository<HiradDbMonitorLog>
    {
        List<GetAllDBLogStatusByLastRun_Result> GetAllDBLogStatusByLastRun();
    }
}
