using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HiAsgRAS.ViewModel
{
    public class HiradDbMonitorModel
    {
        public HiradDbMonitorModel()
        {
            this.HiradDbMonitorLogs = new HashSet<HiradDbMonitorLogModel>();
        }

        public int Id { get; set; }
        public string Application { get; set; }
        public string DBServer { get; set; }
        public string DbName { get; set; }
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public bool IsMonitor { get; set; }
        public bool IsDeleted { get; set; }
        public int? StatusTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? PreviousStatusTypeId { get; set; }
        public virtual ICollection<HiradDbMonitorLogModel> HiradDbMonitorLogs { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

        public System.DateTime? StatusTypeChangedOn { get; set; }
        public int? NewDbId { get; set; }

        public string ActionMode { get; set; }
        public int? DbServerId { get; set; }
        public string StatusText { get; set; }
         public string SupportStaff { get; set; }
         public string DBServerName { get; set; }
    }
}
