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
    
    public partial class HiradDbMonitorLog
    {
        public long Id { get; set; }
        public int DbMonitorId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public Nullable<System.DateTime> MonitoredAt { get; set; }
        public Nullable<System.DateTime> LoggedAt { get; set; }
    
        public virtual HiradDbMonitor HiradDbMonitor { get; set; }
    }
}
