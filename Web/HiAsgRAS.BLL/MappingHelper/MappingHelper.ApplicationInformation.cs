using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.MappingHelper
{
    public partial class MappingHelper
    {
        internal static List<ApplicationInformationModel> MapApplicationInfoListToModels(
                   IList<ApplicationInfomation> lstEntityRows)
        {
            List<ApplicationInformationModel> lstModel = new List<ApplicationInformationModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapApplicationInfoEntityToViewModel(objEntity));
            }

            return lstModel;
        }

        internal static ApplicationInfomation MapApplicationInfoModelToEntity(ApplicationInformationModel viewModel)
        {
            return new ApplicationInfomation
            {
                Id = viewModel.Id,
                ApplicationId=viewModel.ApplicationId,
                ApplicationName = viewModel.ApplicationName,
                ApplicationInformation = viewModel.ApplicationInformation,
                ApplicationType = viewModel.ApplicationType,    
                Comments=viewModel.Comments,         
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                ModifiedBy = viewModel.ModifiedBy,
                ModifiedDate = viewModel.ModifiedDate,

            };
        }

        internal static ApplicationInformationModel MapApplicationInfoEntityToViewModel(ApplicationInfomation app)
        {
            return new ApplicationInformationModel
            {
                Id = app.Id,
                ApplicationId = app.ApplicationId,
                ApplicationName = app.ApplicationName ?? string.Empty,
                ApplicationInformation = app.ApplicationInformation ?? string.Empty,
                ApplicationType = app.ApplicationType ?? string.Empty,
                Comments = app.Comments ?? string.Empty,
                ModifiedBy = app.ModifiedBy ?? string.Empty,
                ModifiedDate = (app.ModifiedDate),
                CreatedBy = app.CreatedBy ?? string.Empty,
                CreatedDate = (app.CreatedDate)
            };
        }
               
    }
}
