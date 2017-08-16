using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HiAsgRAS.DAL.Repositories
{
    public class HiradDbMonitorRepository : RepositoryBase<HiradDbMonitor>, IHiradDbMonitorRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();
        public IEnumerable<HiradDbMonitorModel> GetDBServerAll()
        {
            var db = RepositoryContext.ObjectContext;
            var dbList = (from rec in db.Set<HiradDbMonitor>()
                              where rec.IsDeleted == false &&
                              rec.StatusTypeId == 1
                              select new HiradDbMonitorModel
                              {
                                  Id = rec.Id,
                                  DBServer = rec.DBServer,
                                  DbName = rec.DbName,
                                  Application = rec.Application, 
                                  IsMonitor = rec.IsMonitor
                              }).ToList();

            return dbList;
        }
        public List<HiradDbList_Result> HiradDbList_Procedure()
        {
            var result = dbEntity.HiradDbList();
            return result.ToList();
        }
        public GetDBDetailsById_Result GetDbDetails(int id)
        {

            //var db = RepositoryContext.ObjectContext;
            //var appDetail = (from s in db.Set<HiradDbMonitor>()
            //                 where s.Id == id
            //                 select s).FirstOrDefault();

            var result = dbEntity.GetDBDetailsById(id).FirstOrDefault();  
            return result;
        }
        public bool CheckDuplicateDatabase(HiradDbMonitorModel hiradDbMonitorModel)
        {
            var recs = GetAll(x => (x.Application.Trim().ToUpper() == hiradDbMonitorModel.Application.Trim().ToUpper() &&
                x.DbName.Trim().ToUpper() == hiradDbMonitorModel.DbName.Trim().ToUpper() &&
                x.DbServerId == hiradDbMonitorModel.DbServerId && x.IsDeleted == false)).ToList();

            if (hiradDbMonitorModel.Id > 0)
            {
                recs = GetAll(x => (x.Application.Trim().ToUpper() == hiradDbMonitorModel.Application.Trim().ToUpper() &&
                x.DbName.Trim().ToUpper() == hiradDbMonitorModel.DbName.Trim().ToUpper() &&
                x.DbServerId == hiradDbMonitorModel.DbServerId && x.IsDeleted == false &&
                                  x.Id != hiradDbMonitorModel.Id)).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }
    }
}
