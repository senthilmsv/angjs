using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace HiAsgRAS.BLL.MappingHelper
{
    public partial class MappingHelper
    {
        internal static List<HiradServerLogModel> MapHiradServerLogEntitiesToModels(IList<HiradServerLog> lstEntities)
        {
            if (lstEntities.Any())
            {
                return (from objEntity in lstEntities
                        select MapHiradServerLogModelToEntity(objEntity)
                        ).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                return new List<HiradServerLogModel>();
            }
        }

        internal static HiradServerLogModel MapHiradServerLogModelToEntity(HiradServerLog viewModel)
        {
            return new HiradServerLogModel
            {
                AvblHDDSpace = viewModel.AvblHDDSpace,
                AvblRAM = viewModel.AvblRAM,
                ErrorDescription = viewModel.ErrorDescription,
                Id = viewModel.Id,
                LoggedAt = viewModel.LoggedAt,
                MonitoredAt = viewModel.MonitoredAt,
                ServerId = viewModel.ServerId,
                Status = viewModel.Status
            };
        }        
    }
}
