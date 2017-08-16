using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class DbMonitorLogStatusByLastRunModel
    {
        public int DbMonitorId { get; set; }
        public string Application { get; set; }
        public string DbName { get; set; }
        public string DBServerName { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string MonitoredAt { get; set; }
        public DateTime? LoggedAt { get; set; }
    }
}
