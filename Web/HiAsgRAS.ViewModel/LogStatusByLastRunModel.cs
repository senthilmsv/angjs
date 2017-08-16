using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class LogStatusByLastRunModel
    {
        public int ServerId { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string MonitoredAt { get; set; }
        public string AvblRAM { get; set; }
        public string AvblHDDSpace { get; set; }
        public System.DateTime? LoggedAt { get; set; }
        public string SystemName { get; set; }
        public int StatusPercent { get; set; }
        public string IPAddress { get; set; }
        public string HDDConfiguration { get; set; }

        public decimal HddPercentage { get; set; }

        /// <summary>
        /// Total RAM
        /// </summary>
        public string RAM { get; set; }

        public decimal RAMPercentage { get; set; }

        public DateTime? LastBootTime { get; set; }
        public int LastBootInDays { get; set; }
    }
}
