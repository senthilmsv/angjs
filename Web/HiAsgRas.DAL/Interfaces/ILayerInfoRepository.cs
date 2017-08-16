using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.DAL.Interfaces;
using HiAsgRAS.ViewModel;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface ILayerInfoRepository : IRepository<LayerInfo>
    {
        IEnumerable<LayerInfoModel> GetLayerList();
        LayerInfo GetLayerDetails(int Id);
        bool CheckDuplicateLayerName(LayerInfoModel layerModel);
        //int UpdateIsActive(int Id, string status);
        List<AppsListModel> GetAllAppsByLayerId(int layerId);
    }
}
