using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
    public class BAOInfoModel
    {
        public int Id { get; set; }
        public string BAOwnerPrimary { get; set; }
        public string BAPhonePrimary { get; set; }
        public string BAEmailPrimary { get; set; }
        public string BAODeptPrimary { get; set; }
        public string BAOwnerSecondary { get; set; }
        public string BAPhoneSecondary { get; set; }
        public string BAEmailSecondary { get; set; }
        public string BAODeptSecondary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
       // public string IsActive { get; set; }

        public int? NewBAOId { get; set; }

        public string ActionMode { get; set; }
       

        public ApplicationInformationModel ApplicationInformation { get; set; }

    }
}
