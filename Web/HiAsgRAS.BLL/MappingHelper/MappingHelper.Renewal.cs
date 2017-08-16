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
        public static List<RenewalViewModel> MapRenewalEntitiesToModels(List<RenewalDetail> lstEntities)
        {
            if (lstEntities.Any())
            {
                return (from objEntity in lstEntities
                        select MapRenewalDetailEntityToViewModel(objEntity)).ToList();
            }
            else
            {
                return new List<RenewalViewModel>();
            }
        }

        internal static RenewalDetail MapRenewalDetailModelToEntity(RenewalViewModel viewModel)
        {
            return new RenewalDetail
            {
                Id = viewModel.Id,
                ApplicationId = viewModel.ApplicationId,
                IsRenewalRequired = viewModel.IsRenewalRequired,
                UniqueId=viewModel.UniqueId,
                CreatedDate=viewModel.CreatedDate
                //Id = viewModel.Id,
                //WebFolder = viewModel.WebFolder,
                //WebSite = viewModel.WebSite,
                //WebStat = viewModel.WebStat,
                //Active = viewModel.Active,
                //AppServer = viewModel.AppServer,
                //DBServer = viewModel.DBServer,
                //Status = viewModel.Status,
                //ABCID = viewModel.ABCID,
                //RemedyGroupName = viewModel.RemedyGroupName,
                //PrimayContact = viewModel.PrimayContact,
                //SecondaryContact = viewModel.SecondaryContact,
                //ProdSupportAgreement = viewModel.ProdSupportAgreement,
                //BPContact = viewModel.BPContact,
                //Description = viewModel.Description,
                //BAOId = viewModel.BAOId,
                //ModifiedBy = viewModel.ModifiedBy,
                //ModifiedDate = viewModel.ModifiedDate,
                //CreatedBy = viewModel.CreatedBy,
                //CreatedDate = viewModel.CreatedDate,
                //HiradNew = Convert.ToBoolean(true)


            };
        }

        internal static RenewalViewModel MapRenewalDetailEntityToViewModel(RenewalDetail app)
        {
            return new RenewalViewModel
            {
                Id = app.Id,
                ApplicationId = app.ApplicationId,
                IsRenewalRequired = app.IsRenewalRequired,
                UniqueId=app.UniqueId,
                CreatedDate=app.CreatedDate
                //Id = app.Id,
                //WebFolder = app.WebFolder ?? string.Empty,
                //WebSite = app.WebSite ?? string.Empty,
                //WebStat = app.WebStat ?? string.Empty,
                //Active = app.Active ?? string.Empty,
                //DBServer = app.DBServer ?? string.Empty,
                //AppServer = app.AppServer ?? string.Empty,
                //Status = app.Status ?? string.Empty,
                //ABCID = app.ABCID ?? string.Empty,
                //RemedyGroupName = app.RemedyGroupName ?? string.Empty,
                //PrimayContact = app.PrimayContact ?? string.Empty,
                //SecondaryContact = app.SecondaryContact ?? string.Empty,
                //ProdSupportAgreement = app.ProdSupportAgreement ?? string.Empty,
                //BPContact = app.BPContact ?? string.Empty,
                //Description = app.Description ?? string.Empty,
                //// HiradNew = Convert.ToBoolean(app.HiradNew)
                //ModifiedBy = app.ModifiedBy ?? string.Empty,
                //ModifiedDate = Convert.ToDateTime(app.ModifiedDate),
                //CreatedBy = app.CreatedBy ?? string.Empty,
                //CreatedDate = Convert.ToDateTime(app.CreatedDate)
            };
        }
    }
}
