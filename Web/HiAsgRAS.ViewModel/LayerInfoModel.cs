using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.ViewModel
{
   public class LayerInfoModel
    {
        public int Id { get; set; }
        public string AppLayerName { get; set; }
        public string LayerLocation { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<LayerInfoModel> AppList { get; set; }
       // public string IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int? NewLayerId { get; set; }

        public string ActionMode { get; set; }
    }
}
