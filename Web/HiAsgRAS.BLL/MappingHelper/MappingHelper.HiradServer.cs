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
        internal static HiradServer MapSeverViewModelToEntity(HiradServerModel viewModel)
        {
            return new HiradServer
            {
                Id = viewModel.Id,
                SystemName = viewModel.SystemName,
                Location = viewModel.Location,
                SerialNumber = viewModel.SerialNumber,
                AssetTag = viewModel.AssetTag,
                ABCId = viewModel.ABCId,
                CostCenter = viewModel.CostCenter,
                SupportStaff = viewModel.SupportStaff,
                Model = viewModel.Model,
                Platform = viewModel.Platform,
                BuildVersion = viewModel.BuildVersion,
                IPAddress = viewModel.IPAddress,
                Processor = viewModel.Processor,
                TotalCores = viewModel.TotalCores,
                HDDConfiguration = viewModel.HDDConfiguration,
                RAM = viewModel.RAM,
                TSMInstalled = viewModel.TSMInstalled,
                Comments = viewModel.Comments,
                HiradNew = viewModel.HiradNew,
                LastBootTime = viewModel.LastBootTime,
               // Domain = viewModel.Domain,
                IsDeleted = viewModel.IsDeleted,
                IsMonitor = viewModel.IsMonitor,
                ModifiedBy = viewModel.ModifiedBy,
                ModifiedDate = viewModel.ModifiedDate,
                CreatedBy = viewModel.CreatedBy,
                CreatedDate = viewModel.CreatedDate,
                StatusTypeId = viewModel.StatusTypeId,
                StatusTypeChangedOn = viewModel.StatusTypeChangedOn,
                NewServerId = viewModel.NewServerId,
                ApplicationUse = viewModel.ApplicationUse
            };
        }

        internal static HiradServerModel MapSeverEntityToViewModel(HiradServer objEntity)
        {
            return new HiradServerModel()
            {
                Id = objEntity.Id,
                SystemName = objEntity.SystemName,
                Location = objEntity.Location,
                UnitSize = objEntity.UnitSize ?? string.Empty,
                SerialNumber = objEntity.SerialNumber ?? string.Empty,
                AssetTag = objEntity.AssetTag ?? string.Empty,
                PO_ = objEntity.PO_ ?? string.Empty,
                Field7 = objEntity.Field7 ?? string.Empty,
                ABCId = objEntity.ABCId ?? string.Empty,
                CostCenter = objEntity.CostCenter ?? string.Empty,
                SupportStaff = objEntity.SupportStaff ?? string.Empty,
                Model = objEntity.Model ?? string.Empty,
                Platform = objEntity.Platform ?? string.Empty,
                BuildVersion = objEntity.BuildVersion ?? string.Empty,
                Configuration = objEntity.Configuration ?? string.Empty,
                IPAddress = objEntity.IPAddress ?? string.Empty,
                ILOIPAddress = objEntity.ILOIPAddress ?? string.Empty,
                IPAddressOther = objEntity.IPAddressOther ?? string.Empty,
                ApplicationUse = objEntity.ApplicationUse ?? string.Empty,
                Function = objEntity.Function ?? string.Empty,
                Processor = objEntity.Processor ?? string.Empty,
                TotalCores = Convert.ToDouble(objEntity.TotalCores),
                Storage = objEntity.Storage ?? string.Empty,

                HDDConfiguration = objEntity.HDDConfiguration ?? string.Empty,
                RAM = objEntity.RAM ?? string.Empty,
                WINTELPatchSchedule = objEntity.WINTELPatchSchedule ?? string.Empty,
                TSMInstalled = objEntity.TSMInstalled ?? string.Empty,
                TSMConfigurationPath = objEntity.TSMConfigurationPath ?? string.Empty,
                TSMBackupWindow = objEntity.TSMBackupWindow ?? string.Empty,
                TSMTicket_ = objEntity.TSMTicket_ ?? string.Empty,
                IPRequestTicket_ = objEntity.IPRequestTicket_ ?? string.Empty,
                CableRunTicket_ = objEntity.CableRunTicket_ ?? string.Empty,
                PortActivationTicket_ = objEntity.PortActivationTicket_ ?? string.Empty,
                Rack_Stack_ = objEntity.Rack_Stack_ ?? string.Empty,
                BuildTicket = objEntity.BuildTicket ?? string.Empty,
                MoveRFC = objEntity.MoveRFC ?? string.Empty,
                TivoliMonitoringLicenseApproval_ = objEntity.TivoliMonitoringLicenseApproval_ ?? string.Empty,
                InstallTivoliEnhancedProfile_ = objEntity.InstallTivoliEnhancedProfile_ ?? string.Empty,
                IBMToolsSRM_Parity = objEntity.IBMToolsSRM_Parity ?? string.Empty,
                QATicket_ = objEntity.QATicket_ ?? string.Empty,
                DecommTicket_ = objEntity.DecommTicket_ ?? string.Empty,
                Rack_StackDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(objEntity.Rack_StackDate),

                BuildDate = objEntity.BuildDate ?? string.Empty,
                PatchFolderInstall = objEntity.PatchFolderInstall ?? string.Empty,
                TSMexeInstall = objEntity.TSMexeInstall ?? string.Empty,
                ArrayConfig = objEntity.ArrayConfig ?? string.Empty,
                NewServerForm = objEntity.NewServerForm ?? string.Empty,
                SCALLANBKUP = objEntity.SCALLANBKUP ?? string.Empty,
                ISSDatabaseForm = objEntity.ISSDatabaseForm ?? string.Empty,
                UpdatedAssetCenter = objEntity.UpdatedAssetCenter ?? string.Empty,
                QASubmmissionDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(objEntity.QASubmmissionDate) ,
                QAPassDate = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(objEntity.QAPassDate) ,
                Comments = objEntity.Comments ?? string.Empty,
                VMHost = objEntity.VMHost ?? string.Empty,
                ModifiedBy = objEntity.ModifiedBy ?? string.Empty,
                ModifiedDate = (objEntity.ModifiedDate),
                CreatedBy = objEntity.CreatedBy ?? string.Empty,
                CreatedDate = (objEntity.CreatedDate),
                HiradNew = Convert.ToBoolean(objEntity.HiradNew),

                //OriginalInstallDate = Convert.ToDateTime(objEntity.OriginalInstallDate),
                LastBootTime = HiAsgRAS.Common.NullHandler.AvoidNullDateTimeNullable(objEntity.LastBootTime),
                //SystemType = objEntity.SystemType ?? string.Empty,
              //  Domain = objEntity.Domain ?? string.Empty,
                IsMonitor = objEntity.IsMonitor ?? false,
                IsDeleted = objEntity.IsDeleted,
                StatusTypeId = objEntity.StatusTypeId,
                StatusTypeChangedOn = objEntity.StatusTypeChangedOn,
                NewServerId = objEntity.NewServerId
            };
        }
    }
}
