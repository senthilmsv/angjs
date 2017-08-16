using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.Repositories;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.DAL.EntityModels;

namespace HiAsgRAS.BLL
{
   public class HiradWebBLL: IHiradWebBLL
    {
       private IHiradWebRepository _IHiradWebRepository;
       private IHiradWebLogRepository _IHiradWebLogRepository;

       public HiradWebBLL(IHiradWebRepository hiradWebRepository, 
           IHiradWebLogRepository hiradWebLogRepository)
        {
            _IHiradWebRepository = hiradWebRepository;
            _IHiradWebLogRepository = hiradWebLogRepository;
        }

       public List<HiradWebModel> searchHiradWebAppList_procedure(HiradWebModel hiradModel)
       {
           //hiradModel.WebSiteType = "Web";
           HiradWebAppListSearch_Result objEntity = new HiradWebAppListSearch_Result();
           objEntity.WebFolder = hiradModel.WebFolder;
           objEntity.Active = hiradModel.Active;
           objEntity.Status = hiradModel.Status;
           objEntity.RemedyGroupName = hiradModel.RemedyGroupName;
           objEntity.WebSite = hiradModel.WebSite;
           objEntity.WebStat = hiradModel.WebStat;
           objEntity.ABCID = hiradModel.ABCID;
           objEntity.AppServer = hiradModel.AppServer;

           var result = (from c in _IHiradWebRepository.searchHiradWebAppList_Procedure(objEntity)
                         where c.WebSiteType.ToLower() == hiradModel.WebSiteType.ToLower()
                         select new HiradWebModel
                         {
                             Id = c.Id,
                             WebFolder = c.WebFolder ?? string.Empty,
                             WebSite = c.WebSite ?? string.Empty,
                             RemedyGroupName = c.RemedyGroupName ?? string.Empty,
                             Active = c.Active ?? string.Empty,
                             Status = c.Status ?? string.Empty,
                             PrimayContact = c.PrimayContact ?? string.Empty,
                             WebStat = c.WebStat ?? string.Empty,
                             ABCID = c.ABCID ?? string.Empty,
                             BPContact = c.BPContact ?? string.Empty,
                             ProdSupportAgreement = c.ProdSupportAgreement ?? string.Empty,
                             Description = c.Description ?? string.Empty,
                             DBServer = c.DBServer ?? string.Empty,
                             AppServer = c.AppServer ?? string.Empty,
                             SecondaryContact = c.SecondaryContact ?? string.Empty,
                             BAOwnerPrimary=c.BAOwnerPrimary??string.Empty,
                             BAODeptPrimary=c.BAODeptPrimary??string.Empty,
                             APPServerText=c.AppServerText??string.Empty,
                             DBServerText=c.DbServerText??string.Empty
                         }).ToList();

           return result;
       }


       public HiradWebModel GetWebAppDetails(int id)
       {
           //var appEnity = _IHiradWebRepository.GetWebAppDetails(id);
           //return MappingHelper.MappingHelper.MapWebAppEntityToViewModel(appEnity);

           GetWebAppDetailsById_Result app = new GetWebAppDetailsById_Result();

           app = _IHiradWebRepository.GetWebAppDetails(id);

            return new HiradWebModel
            {
                Id = app.Id,
                WebFolder = app.WebFolder ?? string.Empty,
                WebSite = app.WebSite ?? string.Empty,
                WebStat = app.WebStat ?? string.Empty,
                WebSiteType = app.WebSiteType ?? string.Empty,
                Active = app.Active ?? string.Empty,
                DBServer = app.DBServer ?? string.Empty,
                AppServer = app.AppServer ?? string.Empty,
                Status = app.Status ?? string.Empty,
                ABCID = app.ABCID ?? string.Empty,
                RemedyGroupName = app.RemedyGroupName ?? string.Empty,
                PrimayContact = app.PrimayContact ?? string.Empty,
                SecondaryContact = app.SecondaryContact ?? string.Empty,
                ProdSupportAgreement = app.ProdSupportAgreement ?? string.Empty,
                Description = app.Description ?? string.Empty,
                HiradNew = Convert.ToBoolean(app.HiradNew),
                ModifiedBy = app.ModifiedBy ?? string.Empty,
                ModifiedDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ModifiedDate),
                CreatedBy = app.CreatedBy ?? string.Empty,
                CreatedDate =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.CreatedDate),
                DBName = app.DBName ?? string.Empty,
                BPContact = app.BPContact ?? string.Empty,
                BPDept = app.BPDept ?? string.Empty,
                BPEmail = app.BPEmail ?? string.Empty,
                BPPhone = app.BPPhone ?? string.Empty,
                BAOId = app.BAOId,
                AppServerId = app.AppServerId,
                DbServerId = app.DbServerId,
                StatusTypeId = app.StatusTypeId,
                StatusTypeChangedOn =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.StatusTypeChangedOn),
                IsMonitor = app.IsMonitor,
                IsRenewal = app.IsRenewal,
                ApplicationRenewalDate =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ApplicationRenewalDate),
                APPServerText=app.AppServerText?? string.Empty,
                DBServerText=app.DbServerText?? string.Empty,
                
            };
       }

       public int UpdateWebApp(HiradWebModel HiradWebModel)
       {
           int webAppId = HiradWebModel.Id;

           var appEntity = MappingHelper.MappingHelper.MapWebAppModelToEntity(HiradWebModel);

           if (appEntity.Id > 0)
           {
               //Update existing server details
               _IHiradWebRepository.Update(appEntity);
           }
           else
           {
               _IHiradWebRepository.Add(appEntity);

           }

           _IHiradWebRepository.SaveChanges();

           return webAppId;
       }

       public void DeleteWebApp(int id, string modified)
       {
           //_IHiradWebRepository.DeleteById(id);
           //_IHiradWebRepository.SaveChanges();
           var entity = _IHiradWebRepository.GetById(id);
           if (entity != null)
           {
               entity.IsDeleted = true;
               entity.ModifiedBy = modified;
               entity.ModifiedDate = DateTime.Now;
               _IHiradWebRepository.Update(entity);
               _IHiradWebRepository.SaveChanges();
           }
       }

       #region InterfaceImplementation
       public HiradWebModel GetById(long Id)
       {
           throw new NotImplementedException();
       }

       public void Add(HiradWebModel viewModel)
       {
           throw new NotImplementedException();
       }

       public void Delete(HiradWebModel viewModel)
       {
           throw new NotImplementedException();
       }

       public void DeleteById(long Id)
       {
           throw new NotImplementedException();
       }

       public void Update(HiradWebModel viewModel)
       {
           throw new NotImplementedException();
       }

       public IList<HiradWebModel> GetAll()
       {
           throw new NotImplementedException();
       }

       public IQueryable<HiradWebModel> GetQueryable()
       {
           throw new NotImplementedException();
       }


       #endregion InterfaceImplementation


       public List<WebsiteLogStatusByLastRunModel> GetAllWebLogStatusByLastRun()
       {
           List<WebsiteLogStatusByLastRunModel> lstLastRun = new List<WebsiteLogStatusByLastRunModel>();
           List<GetAllWebsiteLogStatusByLastRun_Result> recs = 
               _IHiradWebLogRepository.GetAllWebsiteLogStatusByLastRun();
           if (recs != null)
           {
               WebsiteLogStatusByLastRunModel objModel = null;
               foreach (var objRow in recs)
               {
                   objModel = new WebsiteLogStatusByLastRunModel()
                   {
                       WebId = objRow.WebId,
                       WebFolder = objRow.WebFolder,
                       WebSite = objRow.WebSite,
                       PrimayContact = objRow.PrimayContact,
                       SecondaryContact = objRow.SecondaryContact,
                       BPContact = objRow.BPContact,
                       BPDept = objRow.BPDept,
                       BPPhone = objRow.BPPhone,
                       BPEmail = objRow.BPEmail,
                       Description = objRow.Description,
                       Status = objRow.Status,
                       ErrorDescription = objRow.ErrorDescription,                       
                       MonitoredAt = objRow.MonitoredAt.Value.ToString("MM/dd/yyyy hh:mm tt"),
                       LoggedAt = objRow.LoggedAt
                   };
                   lstLastRun.Add(objModel);
               }
           }

           return lstLastRun;
       }


       public HiradWebModel GetAllWebLogsByWebsite(int id)
       {
           HiradWebModel objWebModel = GetWebAppDetails(id);
           if (objWebModel != null)
           {
               //Log Rows
               var lstEntityRows = _IHiradWebLogRepository.GetAll(x => x.WebId == id);
               objWebModel.HiradWebLogs = new List<HiradWebLogModel>();

               HiradWebLogModel objModel = new HiradWebLogModel();
               foreach (var objRow in lstEntityRows)
               {
                   objModel = new HiradWebLogModel()
                   {
                       Id = objRow.Id,
                       WebId = objRow.WebId,
                       Status = objRow.Status,
                       ErrorDescription = objRow.ErrorDescription,
                       MonitoredAt = objRow.MonitoredAt.Value.ToString("MM/dd/yyyy hh:mm tt"),
                       LoggedAt = objRow.LoggedAt
                   };
                   objWebModel.HiradWebLogs.Add(objModel);
               }
           }
           return objWebModel;
       }


       public List<HiradWebModel> GetAllSPSites()
       {
           var lstEntityRows = _IHiradWebRepository.GetAll(x => x.WebSiteType.Equals("SP") && x.IsDeleted == false);
           return MappingHelper.MappingHelper.MapWebAppEntitiesToViewModels(lstEntityRows);
       }

       public IEnumerable<HiradWebModel> GetWebAll()
       {
           var recs = _IHiradWebRepository.GetWebAll();
           return recs;
       }

       public bool CheckDuplicateWebApp(HiradWebModel hiradWebModel)
       {
           return _IHiradWebRepository.CheckDuplicateWebApp(hiradWebModel);
       }
    }
}
