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


        internal static List<ApplicationEntityModel> MapApplicationEntitiesListToModels(
                    IList<ApplicationEntity> lstEntityRows)
        {
            List<ApplicationEntityModel> lstModel = new List<ApplicationEntityModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapApplicationEntityToViewModel(objEntity));
            }

            return lstModel;
        }

        private static ApplicationEntityModel MapApplicationEntityToViewModel(ApplicationEntity objEntity)
        {
            return new ApplicationEntityModel
            {
                Id = objEntity.Id,
                Name = objEntity.Name,
                Description = objEntity.Description
            };
        }
    }
}
