using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace HiAsgRAS.ViewModel
{
    public class HiradWebModel
    {
        public HiradWebModel()
        {
            this.HiradWebLogs = new HashSet<HiradWebLogModel>();
        }
        public int Id { get; set; }
        public string WebFolder { get; set; }
        public string WebSite { get; set; }
        public string WebStat { get; set; }
        public string WebSiteType { get; set; }
        public string Active { get; set; }
        public string Status { get; set; }
        public string ABCID { get; set; }
        public string AppServer { get; set; }
        public string DBServer { get; set; }
        public string RemedyGroupName { get; set; }
        public string PrimayContact { get; set; }
        public string SecondaryContact { get; set; }
        public string BPContact { get; set; }
        public string BPDept { get; set; }
        public string BPPhone { get; set; }
        public string BPEmail { get; set; }
        public string Description { get; set; }
        public string ProdSupportAgreement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool HiradNew { get; set; }
        public int? BAOId { get; set; }
        public string DBName { get; set; }
        public bool IsMonitor { get; set; }
        public virtual ICollection<HiradWebLogModel> HiradWebLogs { get; set; }
        public int? StatusTypeId { get; set; }
        public int? PreviousStatusTypeId { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }

        public System.DateTime? StatusTypeChangedOn { get; set; }
        public int? NewWebAppId { get; set; }

        public string ActionMode { get; set; }
        public int? AppServerId { get; set; }
        public int? DbServerId { get; set; }

        public bool? IsRenewal { get; set; }
          public DateTime? ApplicationRenewalDate { get; set; }
          public string BAOwnerPrimary { get; set; }
          public string BAODeptPrimary { get; set; }
          public string APPServerText { get; set; }
          public string DBServerText { get; set; }
    }
}
