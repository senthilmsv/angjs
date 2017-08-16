using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class SystemInformationModel
    {
        public int ServerId { get; set; }
        public string ComputerName { get; set; }
        public string IPAddress { get; set; }
        public string OsName { get; set; }
        public string OsVersion { get; set; }
        //public DateTime OriginalInstallDate { get; set; }
        public DateTime LastBootTime { get; set; }
        public string LastBootTimeText { get; set; }
        public string SystemModel { get; set; }
        //public string SystemType { get; set; }
        public string Processor { get; set; }
        public Int32 TotalCores { get; set; }
        public string TotalRAM { get; set; }
        public string AvblRAM { get; set; }
      //  public string Domain { get; set; }
        public string TotalHDD { get; set; }
        public string AvblHDD { get; set; }

        public string ErrorInfo { get; set; }
        public Exception Ex { get; set; }
    }
}
