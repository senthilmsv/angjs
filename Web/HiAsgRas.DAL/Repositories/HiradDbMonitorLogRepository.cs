using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class HiradDbMonitorLogRepository : RepositoryBase<HiradDbMonitorLog>, IHiradDbMonitorLogRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();
        public List<GetAllDBLogStatusByLastRun_Result> GetAllDBLogStatusByLastRun()
        {
            return dbEntity.GetAllDBLogStatusByLastRun().ToList();
        }
    }
}
