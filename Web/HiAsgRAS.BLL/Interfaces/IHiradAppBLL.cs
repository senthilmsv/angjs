using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HiAsgRAS.BLL.Interfaces
{
    public interface IHiradAppBLL : IBaseBLL<HiradAppModel>
    {
        List<HiradAppModel> searchHiradAppList_procedure(HiradAppModel hiradModel);
        int UpdateApp(HiradAppModel hiradAppModel);
        HiradAppModel GetAppDetails(int id);
        void DeleteApp(int id, string modified);
        bool CheckDuplicateApp(HiradAppModel hiradAppModel);
        bool CheckDuplicateLayer(HiradAppModel hiradAppModel);
    }
}
