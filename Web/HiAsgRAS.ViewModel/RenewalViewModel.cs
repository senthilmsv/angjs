using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class RenewalViewModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public bool? IsRenewalRequired { get; set; }
        public string UniqueId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ApplicationName { get; set; }
        public string RenewalDate { get; set; }
        public string OwnerPrimary { get; set; }
        public string MailPrimary { get; set; }
        public string OwnerSecondary { get; set; }
        public string MailSecondary { get; set; }

    }
}
