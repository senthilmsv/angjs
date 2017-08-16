using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class HiradServerLogRepository : RepositoryBase<HiradServerLog>, IHiradServerLogRepository
    {        
        private HiDashEntities dbEntity = new HiDashEntities();

        public HiradServerLogRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }


        public IEnumerable<HiradServerLogStatus_Result> GetAllServerLogStatus(int? serverId, DateTime? fromDate, DateTime? toDate)
        {
            return dbEntity.HiradServerLogStatus(serverId, fromDate, toDate).ToList();
        }


        public List<GetAllServerLogStatusByLastRun_Result> GetAllServerLogStatusByLastRun()
        {
            return dbEntity.GetAllServerLogStatusByLastRun().ToList();
        }


        public List<GetServerLogStatusByDate_Result> GetServerLogStatusByDate(System.DateTime? LoggedAt)
        {
            return dbEntity.GetServerLogStatusByDate(LoggedAt).ToList();
        }
    }
}
