using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class DashboardTopRowModel
    {
        public double uptime { get; set; }
        public string LastMonitoredAt { get; set; }
        public int RAMCount { get; set; }

        public int RebootResetDays { get; set; }
    }
}
