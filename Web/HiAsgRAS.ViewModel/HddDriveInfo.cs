using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class HddDriveInfo
    {
        public string Drive { get; set; }
        public decimal SizeInBytes { get; set; }
        public string DriveWithSpace { get; set; }
        public decimal UsedPercent { get; set; }
    }
}
