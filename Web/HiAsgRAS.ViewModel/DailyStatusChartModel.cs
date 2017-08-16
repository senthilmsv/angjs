using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{

    public class DailyStatusChartModel
    {
        public List<string> labels { get; set; }
        public List<List<ChartDataModel>> series { get; set; }

        public double uptime { get; set; }
        public string LastMonitoredAt { get; set; }
    }
}
