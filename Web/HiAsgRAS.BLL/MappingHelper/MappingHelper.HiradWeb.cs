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
        internal static HiradWeb MapWebAppModelToEntity(HiradWebModel viewModel)
        {
            return new HiradWeb
            {
                Id = viewModel.Id,
                WebFolder = viewModel.WebFolder,
                WebSite = viewModel.WebSite,
                WebStat = viewModel.WebStat,
                WebSiteType = viewModel.WebSiteType,
                Active = viewModel.Active,
                AppServer = viewModel.AppServer,
                DBServer = viewModel.DBServer,
                Status = viewModel.Status,
                ABCID = viewModel.ABCID,
                RemedyGroupName = viewModel.RemedyGroupName,
                PrimayContact = viewModel.PrimayContact,
                SecondaryContact = viewModel.SecondaryContact,
                ProdSupportAgreement = viewModel.ProdSupportAgreement,
                BPContact = viewModel.BPContact,
                Description = viewModel.Description,
                BAOId = viewModel.BAOId,
                ModifiedBy = viewModel.ModifiedBy,
                ModifiedDate = viewModel.ModifiedDate,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                HiradNew = viewModel.HiradNew,
                DBName = viewModel.DBName,
                AppServerId = viewModel.AppServerId,
                DbServerId = viewModel.DbServerId,
                StatusTypeId = viewModel.StatusTypeId,
                StatusTypeChangedOn = viewModel.StatusTypeChangedOn,
                IsMonitor=viewModel.IsMonitor,
                IsRenewal=viewModel.IsRenewal,
                ApplicationRenewalDate=viewModel.ApplicationRenewalDate,
                BPDept=viewModel.BPDept

            };
        }

        internal static HiradWebModel MapWebAppEntityToViewModel(HiradWeb app)
        {
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
                ModifiedDate = (app.ModifiedDate),
                CreatedBy = app.CreatedBy ?? string.Empty,
                CreatedDate = (app.CreatedDate),
                DBName = app.DBName ?? string.Empty,
                BPContact = app.BPContact ?? string.Empty,
                BPDept = app.BPDept ?? string.Empty,
                BPEmail = app.BPEmail ?? string.Empty,
                BPPhone = app.BPPhone ?? string.Empty,
                BAOId = app.BAOId,
                AppServerId = app.AppServerId,
                DbServerId = app.DbServerId,
                StatusTypeId = app.StatusTypeId,
                StatusTypeChangedOn = app.StatusTypeChangedOn,
                IsMonitor=app.IsMonitor,
                IsRenewal=app.IsRenewal,
                ApplicationRenewalDate=app.ApplicationRenewalDate
            };
        }

        internal static List<HiradWebModel> MapWebAppEntitiesToViewModels(IList<HiradWeb> lstEntityRows)
        {
            List<HiradWebModel> lstModel = new List<HiradWebModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapWebAppEntityToViewModel(objEntity));
            }

            return lstModel;
        }
    }
}
