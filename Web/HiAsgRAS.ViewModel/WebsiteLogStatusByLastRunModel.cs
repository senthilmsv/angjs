using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class WebsiteLogStatusByLastRunModel
    {
        public int WebId { get; set; }
        public string WebFolder { get; set; }
        public string WebSite { get; set; }
        public string Active { get; set; }
        public string PrimayContact { get; set; }
        public string SecondaryContact { get; set; }
        public string BPContact { get; set; }
        public string BPDept { get; set; }
        public string BPPhone { get; set; }
        public string BPEmail { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string MonitoredAt { get; set; }
        public System.DateTime? LoggedAt { get; set; }
    }
}
