using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiAsgRAS.ViewModel
{
    public class HiradWebLogModel
    {
        public long Id { get; set; }
        public int WebId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string MonitoredAt { get; set; }
        public Nullable<System.DateTime> LoggedAt { get; set; }

        public virtual HiradWebModel HiradWeb { get; set; }
    }
}
