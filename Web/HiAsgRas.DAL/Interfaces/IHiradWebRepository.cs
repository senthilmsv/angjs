using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradWebRepository : IRepository<HiradWeb>
    {
        List<HiradWebAppListSearch_Result> searchHiradWebAppList_Procedure(HiradWebAppListSearch_Result objEntity);
        GetWebAppDetailsById_Result GetWebAppDetails(int Id);
        IEnumerable<HiradWebModel> GetWebAll();
        bool CheckDuplicateWebApp(HiradWebModel hiradWebModel);
        
    }
}

   