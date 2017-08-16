using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HiAsgRAS.ViewModel
{
    public class SharedPathModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> AppServerId { get; set; }
        public string ServerName { get; set; }
        public string Path { get; set; }
        public Nullable<int> BAOId { get; set; }
        public string BAOwnerPrimary { get; set; }
        public string BAPhonePrimary { get; set; }
        public string BAOwnerSecondary { get; set; }
        public string BAPhoneSecondary { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }        
        public string ActionMode { get; set; }
        public IEnumerable<SelectListItem> AppServers { get; set; }
    }
}
