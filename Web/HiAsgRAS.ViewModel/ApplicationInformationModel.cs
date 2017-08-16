using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class ApplicationInformationModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationType { get; set; }
        public string ApplicationName { get; set; }
        public string WebSite { get; set; }
        public string Server { get; set; }
        public string Version { get; set; }
        public string ApplicationInformation { get; set; }
        public string Comments { get; set; }
        public string RemedyGroupName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
