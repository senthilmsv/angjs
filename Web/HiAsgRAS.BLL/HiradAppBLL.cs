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
    public class HiradAppBLL : IHiradAppBLL
    {
        private IHiradAppRepository _IHiradAppRepository;

        public HiradAppBLL(IHiradAppRepository hiradAppRepository)
        {
            _IHiradAppRepository = hiradAppRepository;
        }
        public List<HiradAppModel> searchHiradAppList_procedure(HiradAppModel hiradModel)
        {
            HiradAppListSearch_Result objEntity = new HiradAppListSearch_Result();
            objEntity.Application = hiradModel.Application;
            objEntity.APPServerName = hiradModel.APPServerName;
            objEntity.Vendor = hiradModel.Vendor;
            objEntity.ApplicationLayer = hiradModel.ApplicationLayer;
            objEntity.ABCID = hiradModel.ABCID;
            objEntity.ApplicationDomain = hiradModel.ApplicationDomain;
            objEntity.WebsiteURL = hiradModel.WebsiteURL;
            objEntity.SATName = hiradModel.SATName;
            objEntity.ApplicationRenewalDate = hiradModel.AppRenewalDateFrom;
            objEntity.ApplicationLiveDate = hiradModel.AppRenewalDateTo; // ApplicationLiveDate used for application renewal TO date

            var result = (from c in _IHiradAppRepository.searchHiradAppList_Procedure(objEntity)
                          where c.IsDeleted == false
                          select new HiradAppModel
                          {
                              Id = c.Id,
                              Application = c.Application ?? string.Empty,
                              APPServerName = c.APPServerName ?? string.Empty,
                              Vendor = c.Vendor ?? string.Empty,
                              ApplicationLayer = c.ApplicationLayer ?? string.Empty,
                              Version = c.Version ?? string.Empty,
                              Layer5Location = c.Layer5Location ?? string.Empty,
                              RemedyGroupName = c.RemedyGroupName ?? string.Empty,
                              ApplicationDomain = c.ApplicationDomain ?? string.Empty,
                              ABCID = c.ABCID ?? string.Empty,
                              ApplicationLiveDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(c.ApplicationLiveDate),
                              ApplicationRenewalDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(c.ApplicationRenewalDate),
                              HospitalApplication = c.HospitalApplication ?? string.Empty,
                              BPInfo = c.BPInfo ?? string.Empty,
                              Comments = c.Comments ?? string.Empty,
                              KnownIssues = c.KnownIssues ?? string.Empty,
                              Description = c.Description ?? string.Empty,
                              SATName = c.SATName ?? string.Empty,
                              RADPOC = c.RADPOC ?? string.Empty,
                              SecondarySupport = c.SecondarySupport ?? string.Empty,
                              LicenseType = c.LicenseType ?? string.Empty,
                              VendorPOC = c.VendorPOC ?? string.Empty,
                              VendorPhone = c.VendorPhone ?? string.Empty,
                              WebsiteURL = c.WebsiteURL ?? string.Empty,
                              Windows1032Tested = c.Windows1032Tested ?? string.Empty,
                              Windows1064Tested = c.Windows1064Tested ?? string.Empty,
                              BAOwnerPrimary = c.BAOwnerPrimary ?? string.Empty,
                              BAODeptPrimary = c.BAODeptPrimary ?? string.Empty,
                              APPServerText = c.AppServerText ?? string.Empty,
                              DBServerText = c.DbServerText ?? string.Empty,
                              StatusTypeId = c.StatusTypeId
                          }).ToList();

            return result;
        }


        public int UpdateApp(HiradAppModel HiradAppModel)
        {
            int AppId = HiradAppModel.Id;

            var appEntity = MappingHelper.MappingHelper.MapAppModelToEntity(HiradAppModel);

            if (appEntity.Id > 0)
            {
                //Update existing server details
                _IHiradAppRepository.Update(appEntity);
            }
            else
            {
                _IHiradAppRepository.Add(appEntity);

            }

            _IHiradAppRepository.SaveChanges();

            return AppId;
        }

        public HiradAppModel GetAppDetails(int id)
        {
            //var appEnity = _IHiradAppRepository.GetAppDetails(id);
            //return MappingHelper.MappingHelper.MapAppEntityToViewModel(appEnity);

            GetClientAppDetailsById_Result app = new GetClientAppDetailsById_Result();
             app = _IHiradAppRepository.GetAppDetails(id);

           return new HiradAppModel
           {
               Id = app.Id,
               Application = app.Application ?? string.Empty,
               Description = app.Description ?? string.Empty,
               Version = app.Version ?? string.Empty,
               Vendor = app.Vendor ?? string.Empty,
               VendorPOC = app.VendorPOC ?? string.Empty,
               VendorPhone = app.VendorPhone ?? string.Empty,
               WebsiteURL = app.WebsiteURL ?? string.Empty,
               ABCID = String.IsNullOrEmpty(app.ABCID) ? string.Empty : app.ABCID,
               RemedyGroupName = app.RemedyGroupName ?? string.Empty,
               APPServerName = app.APPServerName ?? string.Empty,
               DBServerName = app.DBServerName ?? string.Empty,
               ApplicationLayer = app.ApplicationLayer ?? string.Empty,
               SATName = app.SATName ?? string.Empty,
               Layer5Location = app.Layer5Location ?? string.Empty,
               LicenseType = app.LicenseType ?? string.Empty,
               LicenseInformation = app.LicenseInformation ?? string.Empty,
               Windows1032Tested = app.Windows1032Tested ?? string.Empty,
               Windows1064Tested = app.Windows1064Tested ?? string.Empty,
               RADPOC = app.RADPOC ?? string.Empty,
               BPContact = app.BPContact ?? string.Empty,
              ApplicationLiveDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ApplicationLiveDate),
              ApplicationRenewalDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ApplicationRenewalDate),
               HospitalApplication = app.HospitalApplication ?? string.Empty,
               KnownIssues = app.KnownIssues ?? string.Empty,
               Comments = app.Comments ?? string.Empty,
               BPInfo = app.BPInfo ?? string.Empty,
               ApplicationDomain = app.ApplicationDomain ?? string.Empty,
               SecondarySupport = app.SecondarySupport ?? string.Empty,
               BAOId = app.BAOId,
               ApplicationLayerId = app.ApplicationLayerId,
               HiradNew = Convert.ToBoolean(app.HiradNew),
               CreatedBy = app.CreatedBy ?? string.Empty,
              CreatedDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.CreatedDate),
               ModifiedBy = app.ModifiedBy ?? string.Empty,
               ModifiedDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ModifiedDate),
               StatusTypeId = app.StatusTypeId,
              StatusTypeChangedOn = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.StatusTypeChangedOn),
               AppServerId = app.AppServerId,
               DbServerId = app.DbServerId,
               APPServerText = app.AppServerText ?? string.Empty,
               DBServerText = app.DbServerText ?? string.Empty

           };
        }
        public void DeleteApp(int id, string modified)
        {
            //_IHiradAppRepository.DeleteById(id);
            //_IHiradAppRepository.SaveChanges();

            var entity = _IHiradAppRepository.GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedBy = modified;
                entity.ModifiedDate = DateTime.Now;
                _IHiradAppRepository.Update(entity);
                _IHiradAppRepository.SaveChanges();
            }
        }

        public bool CheckDuplicateApp(HiradAppModel hiradAppModel)
        {
            return _IHiradAppRepository.CheckDuplicateApp(hiradAppModel);
        }

        public bool CheckDuplicateLayer(HiradAppModel hiradAppModel)
        {
            return _IHiradAppRepository.CheckDuplicateLayer(hiradAppModel);
        }
        #region InterfaceImplementation
        public HiradAppModel GetById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Add(HiradAppModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(HiradAppModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(HiradAppModel viewModel)
        {
            throw new NotImplementedException();
        }

        public IList<HiradAppModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<HiradAppModel> GetQueryable()
        {
            throw new NotImplementedException();
        }


        #endregion InterfaceImplementation

    }
}
