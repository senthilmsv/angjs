using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class HiradServerLogStatusModel
    {
        public System.DateTime? LoggedOn { get; set; }
        public string DayName { get; set; }
        public int ServerId { get; set; }
        public string SystemName { get; set; }
        public int? Total_count { get; set; }
        public int? success_count { get; set; }
        public int? Failed_count { get; set; }
        public int? Server_Uptime { get; set; }
    }
}
