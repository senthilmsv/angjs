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
    public class ServerDecommissionRepository : RepositoryBase<HiradServer>, IServerDecommissionRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();

        public ServerDecommissionRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<HiradServerModel> GetAllServers()
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<HiradServer>()
                              // where rec.IsDeleted == false && rec.StatusTypeId == 1 && rec.IsMonitor == true
                              where rec.IsDeleted == false && rec.IsMonitor == true
                              orderby rec.SystemName
                              select new HiradServerModel
                              {
                                  Id = rec.Id,
                                  SystemName = rec.SystemName,
                                  StatusTypeId = rec.StatusTypeId
                              }).ToList();

            return serverList;
        }

        public bool SaveServerInfo(string Ids, string source, int newServerId, int oldServerId)
        {
            int result = dbEntity.UpdateServerInfo(Ids, source, newServerId, oldServerId);
            return result == 0 ? true : false;
        }

        public IEnumerable<HiradAppModel> GetAllClientAppsList(int Id)
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<HiradApp>()
                              where rec.IsDeleted == false &&
                              rec.AppServerId == Id && (rec.StatusTypeId == 1 || rec.StatusTypeId == 3)
                              orderby rec.Application
                              select new HiradAppModel
                              {
                                  Id = rec.Id,
                                  Application = rec.Application,
                                  Version = rec.Version,
                                  Server = rec.Server,
                                  ApplicationLayer = rec.ApplicationLayer,
                                  Layer5Location = rec.Layer5Location,
                                  RemedyGroupName = rec.RemedyGroupName,
                                  RADPOC = rec.RADPOC,
                                  BPInfo = rec.BPInfo
                              }).ToList();

            return serverList;
        }


        public IEnumerable<HiradWebModel> GetAllWebList(int Id)
        {
            var db = RepositoryContext.ObjectContext;
            var webList = (from rec in db.Set<HiradWeb>()
                           where rec.IsDeleted == false &&
                          (rec.AppServerId == Id || rec.DbServerId == Id) && (rec.StatusTypeId == 1 || rec.StatusTypeId == 3)
                           orderby rec.WebFolder
                           select new HiradWebModel
                           {
                               Id = rec.Id,
                               WebFolder = rec.WebFolder,
                               Active = (rec.Active.ToLower() == "yes" || rec.Status.ToLower() == "y") ? "Yes" : "No",
                               Status = rec.Status,
                               RemedyGroupName = rec.RemedyGroupName,
                               BPContact = rec.BPContact,
                               BPDept = rec.BPDept,
                               PrimayContact = rec.PrimayContact,
                               SecondaryContact = rec.SecondaryContact
                           }).ToList();

            return webList;
        }




        public IEnumerable<HiradDbMonitorModel> GetDBServerList(int dbserverId)
        {
            var db = RepositoryContext.ObjectContext;
            var dbList = (from rec in db.Set<HiradDbMonitor>()
                          join b in db.Set<StatusType>() on rec.StatusTypeId equals b.Id
                          join c in db.Set<HiradServer>() on rec.DbServerId equals c.Id
                          orderby rec.Application descending
                          where rec.IsDeleted == false &&
                           rec.DbServerId == dbserverId && (rec.StatusTypeId == 1 || rec.StatusTypeId == 3)
                          orderby rec.Application
                          select new HiradDbMonitorModel
                           {
                               Id = rec.Id,
                               DbName = rec.DbName,
                               Application = rec.Application,
                               DBServer = rec.DBServer,
                               StatusText = b.StatusText,
                               SupportStaff = c.SupportStaff

                           }).ToList();

            return dbList;
        }
    }
}
