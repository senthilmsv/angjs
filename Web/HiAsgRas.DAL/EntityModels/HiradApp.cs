//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HiAsgRAS.DAL.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class HiradApp
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
        public Nullable<System.DateTime> DocumentationDate { get; set; }
        public string Categories { get; set; }
        public string RADPOC { get; set; }
        public string SecondarySupport { get; set; }
        public string ApplicationDomain { get; set; }
        public string BPContact { get; set; }
        public Nullable<System.DateTime> ApplicationLiveDate { get; set; }
        public string Layer4SR_ { get; set; }
        public string Layer4Status { get; set; }
        public string HospitalApplication { get; set; }
        public string KnownIssues { get; set; }
        public string Comments { get; set; }
        public string Windows1032Tested { get; set; }
        public string Windows1064Tested { get; set; }
        public string BPInfo { get; set; }
        public Nullable<System.DateTime> ApplicationRenewalDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> HiradNew { get; set; }
        public Nullable<int> BAOId { get; set; }
        public Nullable<int> ApplicationLayerId { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> StatusTypeId { get; set; }
        public Nullable<System.DateTime> StatusTypeChangedOn { get; set; }
        public Nullable<int> AppServerId { get; set; }
        public Nullable<int> DbServerId { get; set; }
    }
}
