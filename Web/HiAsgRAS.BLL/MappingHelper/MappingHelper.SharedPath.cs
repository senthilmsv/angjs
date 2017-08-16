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
        internal static List<SharedPathModel> MapSharedPathEntitiesListToModels(
                    IList<SharedPath> lstEntityRows)
        {
            List<SharedPathModel> lstModel = new List<SharedPathModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapSharedPathEntityToViewModel(objEntity));
            }

            return lstModel;
        }

        private static SharedPathModel MapSharedPathEntityToViewModel(SharedPath objEntity)
        {
            return new SharedPathModel
            {
                Id = objEntity.Id,
                AppServerId = objEntity.AppServerId,
                BAOId = objEntity.BAOId,
                BAOwnerPrimary = "", //ToDo:
                Comments = objEntity.Comments,
                Name = objEntity.Name,
                Path = objEntity.Path,
                ServerName = "HIDSCFACT057", //ToDo:

                CreatedBy = objEntity.CreatedBy,
                CreatedDate = objEntity.CreatedDate,
                ModifiedBy = objEntity.ModifiedBy,
                ModifiedDate = objEntity.ModifiedDate,

                IsDeleted = objEntity.IsDeleted,
            };
        }

        internal static SharedPath MapSharedPathModelToEntity(SharedPathModel sharedPathModel)
        {

            return new SharedPath()
            {
                Id = sharedPathModel.Id,
                Name=sharedPathModel.Name,
                Path=sharedPathModel.Path,
                AppServerId = sharedPathModel.AppServerId,
                BAOId = sharedPathModel.BAOId,
                Comments=sharedPathModel.Comments,
                CreatedBy = sharedPathModel.CreatedBy ?? string.Empty,
                CreatedDate = sharedPathModel.CreatedDate,
                ModifiedBy = sharedPathModel.ModifiedBy ?? string.Empty,
                ModifiedDate = sharedPathModel.ModifiedDate               

            };
        }
    }
}
