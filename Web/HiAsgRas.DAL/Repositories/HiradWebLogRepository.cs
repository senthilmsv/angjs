using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.DAL.Repositories
{
    public class HiradWebLogRepository : RepositoryBase<HiradWebLog>, IHiradWebLogRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();
        public List<GetAllWebsiteLogStatusByLastRun_Result> GetAllWebsiteLogStatusByLastRun()
        {
            return dbEntity.GetAllWebsiteLogStatusByLastRun().ToList();
        }
    }
}
