using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class HiradDbMonitorLogModel
    {
        public long Id { get; set; }
        public int DbMonitorId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string MonitoredAt { get; set; }
        public Nullable<System.DateTime> LoggedAt { get; set; }

        public virtual HiradDbMonitorModel HiradDbMonitor { get; set; }
    }
}
