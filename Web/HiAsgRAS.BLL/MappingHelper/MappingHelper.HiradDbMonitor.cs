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
        internal static HiradDbMonitor MapDbMonitorModelToEntity(HiradDbMonitorModel viewModel)
        {
            return new HiradDbMonitor
            {
                Id = viewModel.Id,
                Application = viewModel.Application,
                DbName = viewModel.DbName,             
                DBServer = viewModel.DBServer,               
                DbServerId = viewModel.DbServerId,
                StatusTypeId = viewModel.StatusTypeId,
                StatusTypeChangedOn = viewModel.StatusTypeChangedOn,
                IsMonitor = viewModel.IsMonitor,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                ModifiedBy = viewModel.ModifiedBy,
                ModifiedDate = viewModel.ModifiedDate,

            };
        }

        internal static HiradDbMonitorModel MapDbMonitorEntityToViewModel(HiradDbMonitor app)
        {
            return new HiradDbMonitorModel
            {
                Id = app.Id,
                Application = app.Application ?? string.Empty,
                DbName = app.DbName ?? string.Empty,
                DBServer = app.DBServer ?? string.Empty,
                DbServerId = app.DbServerId,
                StatusTypeId = app.StatusTypeId,
                StatusTypeChangedOn = app.StatusTypeChangedOn,
                IsMonitor = app.IsMonitor,           
                ModifiedBy = app.ModifiedBy ?? string.Empty,
                ModifiedDate = (app.ModifiedDate),
                CreatedBy = app.CreatedBy ?? string.Empty,
                CreatedDate = (app.CreatedDate)    
            };
        }

        internal static List<HiradDbMonitorModel> MapWebAppEntitiesToViewModels(IList<HiradDbMonitor> lstEntityRows)
        {
            List<HiradDbMonitorModel> lstModel = new List<HiradDbMonitorModel>();
            foreach (var objEntity in lstEntityRows)
            {
                lstModel.Add(MapDbMonitorEntityToViewModel(objEntity));
            }

            return lstModel;
        }
    }
}
