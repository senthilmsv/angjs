using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.ViewModel;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;

namespace HiAsgRAS.BLL.MappingHelper
{
    public partial class MappingHelper
    {
        internal static HiradApp MapAppModelToEntity(HiradAppModel viewModel)
        {
            return new HiradApp
            {
                Id = viewModel.Id,
                Application = viewModel.Application,
                Description = viewModel.Description,
                Version = viewModel.Version,
                Vendor = viewModel.Vendor,
                VendorPOC = viewModel.VendorPOC,
                VendorPhone = viewModel.VendorPhone,
                WebsiteURL = viewModel.WebsiteURL,
                ABCID = viewModel.ABCID,
                RemedyGroupName = viewModel.RemedyGroupName,
                APPServerName = viewModel.APPServerName,
                DBServerName = viewModel.DBServerName,
                ApplicationLayer = viewModel.ApplicationLayer,
                SATName = viewModel.SATName,
                Layer5Location = viewModel.Layer5Location,
                LicenseType = viewModel.LicenseType,
                LicenseInformation = viewModel.LicenseInformation,
                Windows1032Tested = viewModel.Windows1032Tested,
                Windows1064Tested = viewModel.Windows1064Tested,
                RADPOC = viewModel.RADPOC,
                BPContact = viewModel.BPContact,
                ApplicationLiveDate = viewModel.ApplicationLiveDate ?? null,
                ApplicationRenewalDate = viewModel.ApplicationRenewalDate ?? null,
                HospitalApplication = viewModel.HospitalApplication,
                KnownIssues = viewModel.KnownIssues,
                Comments = viewModel.Comments,
                BPInfo = viewModel.BPInfo,
                ApplicationDomain = viewModel.ApplicationDomain,
                SecondarySupport = viewModel.SecondarySupport,
                BAOId = viewModel.BAOId,
                ApplicationLayerId = viewModel.ApplicationLayerId,
                HiradNew = viewModel.HiradNew,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                ModifiedBy = viewModel.ModifiedBy,
                ModifiedDate = viewModel.ModifiedDate,
                StatusTypeId = viewModel.StatusTypeId,
                StatusTypeChangedOn = viewModel.StatusTypeChangedOn,
                AppServerId = viewModel.AppServerId,
                DbServerId = viewModel.DbServerId
            };
        }

        internal static HiradAppModel MapAppEntityToViewModel(HiradApp app)
        {
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
                CreatedDate =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.CreatedDate),
                ModifiedBy = app.ModifiedBy ?? string.Empty,
                ModifiedDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.ModifiedDate),
                StatusTypeId = app.StatusTypeId,
                StatusTypeChangedOn =  HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(app.StatusTypeChangedOn),
                AppServerId = app.AppServerId,
                DbServerId= app.DbServerId
            };
        }
    }
}
