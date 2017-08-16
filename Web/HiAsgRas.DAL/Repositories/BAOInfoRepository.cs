using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.Common;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;

namespace HiAsgRAS.DAL.Repositories
{
    public class BAOInfoRepository:RepositoryBase<BAOInfo>, IBAOInfoRepository
    {
        private HiDashEntities dbEntity = new HiDashEntities();

        public BAOInfoRepository(IRepositoryContext repositoryContext)
            : base(repositoryContext)
        { }

        public IEnumerable<BAOInfoModel> GetBAOList()
        {
            var db = RepositoryContext.ObjectContext;
            var serverList = (from rec in db.Set<BAOInfo>()
                              where rec.IsDeleted == false
                              select new BAOInfoModel
                              {
                                  Id = rec.Id,
                                  BAOwnerPrimary = rec.BAOwnerPrimary,
                                  BAPhonePrimary = rec.BAPhonePrimary,
                                  BAEmailPrimary = rec.BAEmailPrimary,
                                  BAODeptPrimary = rec.BAODeptPrimary,
                                  BAOwnerSecondary = rec.BAOwnerSecondary,
                                  BAPhoneSecondary = rec.BAOwnerSecondary,
                                  BAEmailSecondary = rec.BAEmailSecondary,
                                  BAODeptSecondary = rec.BAODeptSecondary                                
                              }).ToList();

            return serverList;
        }

        public BAOInfo GetBAODetails(int id)
        {

            var db = RepositoryContext.ObjectContext;
            var BAODetail = (from s in db.Set<BAOInfo>()
                                where s.Id == id
                                select s).FirstOrDefault();


            return BAODetail;
        }

        public List<BAOSearch_Result> searchBAOSList_Procedure(BAOSearch_Result objEntity)
        {
            var result = dbEntity.BAOSearch(objEntity.BAOwnerPrimary, objEntity.BAODeptPrimary, objEntity.BAOwnerSecondary,objEntity.BAODeptSecondary);
            return result.ToList();
        }

        public bool CheckDuplicateBAO(BAOInfoModel baoInfoModel)
        {
            var recs = GetAll(x => (x.BAEmailPrimary.Trim().ToUpper() == baoInfoModel.BAEmailPrimary.Trim().ToUpper())).ToList();

            if (baoInfoModel.Id > 0)
            {
                recs = GetAll(x => (x.BAEmailPrimary.Trim().ToUpper() == baoInfoModel.BAEmailPrimary.Trim().ToUpper()) &&
                                         x.Id != baoInfoModel.Id).ToList();
            }

            return recs.Count() > 0 ? true : false;

        }
        //public int UpdateIsActive(int Id, string status)
        //{
        //    int recUpdated;

        //    var db = RepositoryContext.ObjectContext;
        //    var baoEntity = (from s in db.Set<BAOInfo>()
        //                     where s.Id == Id
        //                     select s).FirstOrDefault();
        //    baoEntity.IsActive = status.ToUpper() == "NO" ? "Yes" : "No";
        //    db.Set<BAOInfo>().Attach(baoEntity);
        //    db.Entry(baoEntity).Property(x => x.IsActive).IsModified = true;
        //    recUpdated = db.SaveChanges();
        //    return recUpdated;
        //}


        public List<AppsListModel> GetAllAppsByBAOId(int baoId)
        {
            List<AppsListModel> lstAllAps = new List<AppsListModel>();

            var db = RepositoryContext.ObjectContext;
            var webList = (from rec in db.Set<HiradWeb>()
                           where rec.IsDeleted == false &&
                                 (rec.BAOId == baoId)
                           select new AppsListModel
                           {
                               AppName = rec.WebFolder + string.Empty,
                               RemedyGroupName = rec.RemedyGroupName + string.Empty,
                               DbName = string.Empty
                           }).ToList();

            lstAllAps.AddRange(webList);

            var appsList = (from rec in db.Set<HiradApp>()
                            where rec.IsDeleted == false &&
                                  (rec.BAOId == baoId)
                            select new AppsListModel
                            {
                                AppName = rec.Application + string.Empty,
                                RemedyGroupName = rec.RemedyGroupName + string.Empty,
                                DbName = string.Empty
                            }).ToList();

            lstAllAps.AddRange(appsList);

          
            return lstAllAps.ToList();
        }

        public BAOInfoModel GetBaoInfoByApplication(int appId, string appType)
        {
            var db = RepositoryContext.ObjectContext;

            BAOInfoModel baoInfoModel = null;

            if (appType.ToUpper().Equals("CLIENT"))
            {
                baoInfoModel = (from app in db.Set<HiradApp>()
                                join bao in db.Set<BAOInfo>() on app.BAOId equals bao.Id
                                where app.Id == appId
                                select new BAOInfoModel
                                {
                                    ApplicationInformation = new ApplicationInformationModel
                                    {
                                        ApplicationId = app.Id,
                                        ApplicationName = app.Application,
                                        ApplicationType = appType,
                                        RemedyGroupName = app.RemedyGroupName,
                                        Version = app.Version
                                    },
                                    Id = bao.Id,
                                    BAOwnerPrimary = bao.BAOwnerPrimary,
                                    BAPhonePrimary = bao.BAPhonePrimary,
                                    BAEmailPrimary = bao.BAEmailPrimary,
                                    BAODeptPrimary = bao.BAODeptPrimary,
                                    BAOwnerSecondary = bao.BAOwnerSecondary,
                                    BAPhoneSecondary = bao.BAPhoneSecondary,
                                    BAEmailSecondary = bao.BAEmailSecondary,
                                    BAODeptSecondary = bao.BAODeptSecondary
                                }).FirstOrDefault();
            }
            else if (appType.ToUpper().Equals("WEB") || appType.ToUpper().Equals("SP"))
            {
                baoInfoModel = (from app in db.Set<HiradWeb>()
                                join bao in db.Set<BAOInfo>() on app.BAOId equals bao.Id
                                where app.Id == appId
                                select new BAOInfoModel
                                {
                                    ApplicationInformation = new ApplicationInformationModel
                                    {
                                        ApplicationId = app.Id,
                                        ApplicationName = app.WebFolder,
                                        ApplicationType = appType,
                                        RemedyGroupName = app.RemedyGroupName,
                                        WebSite = app.WebSite
                                    },
                                    Id = bao.Id,
                                    BAOwnerPrimary = bao.BAOwnerPrimary,
                                    BAPhonePrimary = bao.BAPhonePrimary,
                                    BAEmailPrimary = bao.BAEmailPrimary,
                                    BAODeptPrimary = bao.BAODeptPrimary,
                                    BAOwnerSecondary = bao.BAOwnerSecondary,
                                    BAPhoneSecondary = bao.BAPhoneSecondary,
                                    BAEmailSecondary = bao.BAEmailSecondary,
                                    BAODeptSecondary = bao.BAODeptSecondary
                                }).FirstOrDefault();
            }
            return baoInfoModel;

        }

       
    }
}
