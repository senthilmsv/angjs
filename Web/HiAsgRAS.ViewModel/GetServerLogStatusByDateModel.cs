using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class GetServerLogStatusByDateModel
    {
        public int ServerId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public System.DateTime? MonitoredAt { get; set; }
        public string MonitoredTime { get; set; }
        public string AvblRAM { get; set; }
        public string AvblHDDSpace { get; set; }
        public System.DateTime? LoggedAt { get; set; }
        public string SystemName { get; set; }
        public int StatusPercent { get; set; }

        /// <summary>
        /// Total RAM
        /// </summary>
        public string RAM { get; set; }
        public int RAMPercentage { get; set; }
    }
}
