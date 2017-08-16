using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HiAsgRAS.DAL.Repositories
{
    public class HiradServerRepository : RepositoryBase<HiradServer>, IHiradServerRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();

        public HiradServerRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        

        public List<HiradServerListSearch_Result> searchHiradServerList_Procedure(HiradServerListSearch_Result objEntity)
        {
            var result = dbEntity.HiradServerListSearch(objEntity.SystemName, objEntity.Location, objEntity.IPAddress,
                objEntity.Processor, objEntity.Platform, objEntity.ABCId, objEntity.CostCenter, objEntity.SerialNumber);
            return result.ToList();
        }

        public HiradServer GetSeverDetails(int id)
        {
            var db = RepositoryContext.ObjectContext;
            var serverDetail = (from s in db.Set<HiradServer>()
                                where s.Id == id && s.IsDeleted == false
                                select s).FirstOrDefault();

            return serverDetail;
        }

        public IEnumerable<HiradServerModel> GetServerAll()
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<HiradServer>()
                              where rec.IsDeleted == false &&
                              rec.StatusTypeId == 1
                              select new HiradServerModel
                              {
                                  Id = rec.Id,
                                  SystemName = rec.SystemName,
                                  Location = rec.Location,
                                  IPAddress = rec.IPAddress,
                                  SupportStaff = rec.SupportStaff,
                                  ApplicationUse = rec.ApplicationUse,
                                  IsMonitor = rec.IsMonitor?? false
                              }).ToList();

            return serverList;
        }

        public IEnumerable<HiradServerModel> GetAllMonitoringServers()
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<HiradServer>()
                              where rec.IsDeleted == false &&
                                    rec.IsMonitor == true
                              orderby rec.SystemName ascending
                              select new HiradServerModel
                              {
                                  Id = rec.Id,
                                  SystemName = rec.SystemName,
                                  Location = rec.Location,
                                  IPAddress = rec.IPAddress,
                                  SupportStaff = rec.SupportStaff,
                                  ApplicationUse = rec.ApplicationUse
                              }).ToList();

            return serverList;
        }
        public bool UpdateMonitorInfo(string Ids, string source)
        {
            int result = dbEntity.UpdateMonitorInfo(Ids, source);
            return result == 0 ? true : false;
        }

        public List<AppsListModel> GetAllAppsByServerId(int ServerId)
        {
            List<AppsListModel> lstAllAps = new List<AppsListModel>();

            var db = RepositoryContext.ObjectContext;
            var webList = (from rec in db.Set<HiradWeb>()
                           where rec.IsDeleted == false &&
                                 (rec.AppServerId == ServerId ||
                                 rec.DbServerId == ServerId)
                           select new AppsListModel
                              {
                                  AppName = rec.WebFolder + string.Empty,
                                  RemedyGroupName = rec.RemedyGroupName + string.Empty,
                                  DbName = string.Empty,
                                  StatusTypeId = rec.StatusTypeId
                              }).ToList();

            lstAllAps.AddRange(webList);

            var appsList = (from rec in db.Set<HiradApp>()
                            where rec.IsDeleted == false &&
                                  (rec.AppServerId == ServerId ||
                                  rec.DbServerId == ServerId)
                            select new AppsListModel
                           {
                               AppName = rec.Application + string.Empty,
                               RemedyGroupName = rec.RemedyGroupName + string.Empty,
                               DbName = string.Empty,
                               StatusTypeId = rec.StatusTypeId
                           }).ToList();

            lstAllAps.AddRange(appsList);

            var dbList = (from rec in db.Set<HiradDbMonitor>()
                          where rec.IsDeleted == false &&
                                (rec.DbServerId == ServerId)
                          select new AppsListModel
                           {
                               AppName = rec.Application + string.Empty,
                               RemedyGroupName = string.Empty,
                               DbName = rec.DbName + string.Empty,
                               StatusTypeId = rec.StatusTypeId
                           }).ToList();

            lstAllAps.AddRange(dbList);

            return lstAllAps.ToList();
        }
        public bool CheckDuplicateSystemName(HiradServerModel hiradServerModel)
        {
            var recs = GetAll(x => (x.SystemName.Trim().ToUpper() == hiradServerModel.SystemName.Trim().ToUpper() && x.IsDeleted == false)).ToList();

            if (hiradServerModel.Id > 0)
            {
                recs = GetAll(x => (x.SystemName.Trim().ToUpper() == hiradServerModel.SystemName.Trim().ToUpper() &&
                                  x.Id != hiradServerModel.Id && x.IsDeleted == false)).ToList();
            }
            return recs.Count() > 0 ? true : false;
        }
    }
}
