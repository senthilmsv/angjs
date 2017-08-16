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


        internal static List<StatusTypeModel> MapStatusTypeEntitiesListToModels(
                    IList<StatusType> lstEntityRows)
        {
            List<StatusTypeModel> lstModel = new List<StatusTypeModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapStatusTypeEntityToViewModel(objEntity));
            }

            return lstModel;
        }

        private static StatusTypeModel MapStatusTypeEntityToViewModel(StatusType objEntity)
        {
            return new StatusTypeModel
            {
                Id = objEntity.Id,
                IsDeleted = objEntity.IsDeleted,
                StatusText = objEntity.StatusText
            };
        }
    }
}
