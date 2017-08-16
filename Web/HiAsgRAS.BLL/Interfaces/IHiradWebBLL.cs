using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HiAsgRAS.BLL.Interfaces
{
    public interface IHiradWebBLL : IBaseBLL<HiradWebModel>
    {
        List<HiradWebModel> searchHiradWebAppList_procedure(HiradWebModel hiradModel);
        int UpdateWebApp(HiradWebModel hiradAppModel);
        HiradWebModel GetWebAppDetails(int id);
        void DeleteWebApp(int id, string modified);

        List<WebsiteLogStatusByLastRunModel> GetAllWebLogStatusByLastRun();
        HiradWebModel GetAllWebLogsByWebsite(int id);
        List<HiradWebModel> GetAllSPSites();

        IEnumerable<HiradWebModel> GetWebAll();
        bool CheckDuplicateWebApp(HiradWebModel hiradWebModel);
    }
}
