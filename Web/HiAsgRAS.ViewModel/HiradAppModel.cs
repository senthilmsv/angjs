using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace HiAsgRAS.ViewModel
{
    public class HiradAppModel
    {

        public int Id { get; set; }
        public string Application { get; set; }
     
        public string Description { get; set; }
        public string Version { get; set; }
        public string Vendor { get; set; }
        public string VendorPOC { get; set; }
        public string VendorPhone { get; set; }
        public string WebsiteURL { get; set; }
        public string ABCID { get; set; }
        public string RemedyGroupName { get; set; }
        public Nullable<int> Server { get; set; }
        public string APPServerName { get; set; }
        public string DBServerName { get; set; }
        public string ApplicationLayer { get; set; }
        public string SATName { get; set; }
        public string Layer5Location { get; set; }
        public string LicenseType { get; set; }
        public string LicenseInformation { get; set; }
        public string Windows7Approved { get; set; }
        public string Windows7InstallStatus { get; set; }
        public string Windows7Remediation_Plan { get; set; }
        public string Windows7Business_Approval { get; set; }
        public string ECSCertified { get; set; }
        public string Windows10Approved { get; set; }
        public string Windows7_64Approved { get; set; }
        public string Windows10_64Approved { get; set; }
        public string Documentation { get; set; }
        public DateTime? DocumentationDate { get; set; }
        public string Categories { get; set; }
        public string RADPOC { get; set; }
        public string SecondarySupport { get; set; }
        public string ApplicationDomain { get; set; }
        public string BPContact { get; set; }          
        public DateTime? ApplicationLiveDate{ get; set; }          
        public string Layer4SR_ { get; set; }
        public string Layer4Status { get; set; }
        public string HospitalApplication { get; set; }
        public string KnownIssues { get; set; }
        public string Comments { get; set; }
        public string Windows1032Tested { get; set; }
        public string Windows1064Tested { get; set; }
        public string BPInfo { get; set; }
         
        public DateTime? ApplicationRenewalDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool HiradNew { get; set; }
        public Nullable<int> BAOId { get; set; }
        public Nullable<int> ApplicationLayerId { get; set; }
        public DateTime? AppRenewalDateFrom { get; set; }
        public DateTime? AppRenewalDateTo { get; set; }

        public int? StatusTypeId { get; set; }
        public IEnumerable<SelectListItem> StatusTypes { get; set; }
        
        public System.DateTime? StatusTypeChangedOn { get; set; }
        public int? NewAppId { get; set; }

        public string ActionMode { get; set; }
        public int? AppServerId { get; set; }
        public int? DbServerId { get; set; }

        public IEnumerable<SelectListItem> ApplicationLayers { get; set; }
        public IEnumerable<SelectListItem> ApplicationEntities { get; set; }

        public int? PreviousStatusTypeId { get; set; }
        public string BAOwnerPrimary { get; set; }
        public string BAODeptPrimary { get; set; }
        public string APPServerText { get; set; }
        public string DBServerText { get; set; }
       
    }
}
