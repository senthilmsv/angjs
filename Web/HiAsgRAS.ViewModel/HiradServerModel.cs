using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiAsgRAS.ViewModel
{
    public class HiradServerModel
    {
        public HiradServerModel()
        {
            this.HiradServerLogs = new HashSet<HiradServerLogModel>();
        }
        public int Id { get; set; }
        public string SystemName { get; set; }
        public string ApplicationName { get; set; }
        public string WebFolder { get; set; }
        public string Location { get; set; }
        public string UnitSize { get; set; }
        public string SerialNumber { get; set; }
        public string AssetTag { get; set; }
        public string PO_ { get; set; }
        public string Field7 { get; set; }
        public string ABCId { get; set; }
        public string CostCenter { get; set; }
        public string SupportStaff { get; set; }
        public string Model { get; set; }
        public string Platform { get; set; }
        public string BuildVersion { get; set; }
        public string Configuration { get; set; }
        public string IPAddress { get; set; }
        public string ILOIPAddress { get; set; }
        public string IPAddressOther { get; set; }
        public string ApplicationUse { get; set; }
        public string Function { get; set; }
        public string Processor { get; set; }
        public double TotalCores { get; set; }
        public string Storage { get; set; }
        public string HDDConfiguration { get; set; }
        public string RAM { get; set; }
        public string WINTELPatchSchedule { get; set; }
        public string TSMInstalled { get; set; }
        public string TSMConfigurationPath { get; set; }
        public string TSMBackupWindow { get; set; }
        public string TSMTicket_ { get; set; }
        public string IPRequestTicket_ { get; set; }
        public string CableRunTicket_ { get; set; }
        public string PortActivationTicket_ { get; set; }
        public string Rack_Stack_ { get; set; }
        public string BuildTicket { get; set; }
        public string MoveRFC { get; set; }
        public string TivoliMonitoringLicenseApproval_ { get; set; }
        public string InstallTivoliEnhancedProfile_ { get; set; }
        public string IBMToolsSRM_Parity { get; set; }
        public string QATicket_ { get; set; }
        public string DecommTicket_ { get; set; }
        public DateTime? Rack_StackDate { get; set; }
        public string BuildDate { get; set; }
        public string PatchFolderInstall { get; set; }
        public string TSMexeInstall { get; set; }
        public string ArrayConfig { get; set; }
        public string NewServerForm { get; set; }
        public string SCALLANBKUP { get; set; }
        public string ISSDatabaseForm { get; set; }
        public string UpdatedAssetCenter { get; set; }
        public DateTime? QASubmmissionDate { get; set; }
        public DateTime? QAPassDate { get; set; }
        public string Comments { get; set; }
        public string VMHost { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool HiradNew { get; set; }
        public IEnumerable<HiradServerModel> ServerList { get; set; }
        public DateTime? OriginalInstallDate { get; set; }
        public string SystemType { get; set; }
        public DateTime? LastBootTime { get; set; }
        //public string Domain { get; set; }

        public bool IsMonitor { get; set; }
        public bool IsDeleted { get; set; }


        public string StatusType { get; set; }
        public int? StatusTypeId { get; set; }
        public int? PreviousStatusTypeId { get; set; }
        
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

        public System.DateTime? StatusTypeChangedOn { get; set; }
        public int? NewServerId { get; set; }

        public string ActionMode { get; set; }

        public virtual ICollection<HiradServerLogModel> HiradServerLogs { get; set; }
        public string StatusText { get; set; }

        public List<AppsListModel> AppsList { get; set; }
    }
}
