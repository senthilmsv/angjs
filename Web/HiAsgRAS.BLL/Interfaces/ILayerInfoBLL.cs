using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.ViewModel;
namespace HiAsgRAS.BLL.Interfaces
{
    public interface ILayerInfoBLL:IBaseBLL<LayerInfoModel>
    {
        IEnumerable<LayerInfoModel> GetLayerList();
        int UpdateLayerInfo(LayerInfoModel layerInfoModel);
        LayerInfoModel GetLayerDetails(int id);
        void DeleteLayer(int id, string modified);
        bool CheckDuplicateLayerName(LayerInfoModel layerInfoModel);
        //int UpdateIsActive(int Id, string status);
        List<AppsListModel> GetAllAppsByLayerId(int layerId);
    }
}
