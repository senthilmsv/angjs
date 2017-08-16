using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiAsgRAS.ViewModel
{
    public class HiradServerLogModel
    {
        public long Id { get; set; }
        public int ServerId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public Nullable<System.DateTime> MonitoredAt { get; set; }
        public string AvblRAM { get; set; }
        public string AvblHDDSpace { get; set; }
        public Nullable<System.DateTime> LoggedAt { get; set; }

        public virtual HiradServerModel HiradServer { get; set; }

    }
}
