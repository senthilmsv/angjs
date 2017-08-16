using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class UserDetailModel
    {
        public int Id { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string NUID { get; set; }
        public string EmailID { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? NewUserId { get; set; }

        public string ActionMode { get; set; }
    }
}
