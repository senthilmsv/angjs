using HiAsgRAS.DAL.EntityModels;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HiAsgRAS.DAL.Interfaces
{
    public interface IHiradAppRepository : IRepository<HiradApp>
    {
        List<HiradAppListSearch_Result> searchHiradAppList_Procedure(HiradAppListSearch_Result objEntity);
        GetClientAppDetailsById_Result GetAppDetails(int Id);
        bool CheckDuplicateApp(HiradAppModel hiradAppModel);
        bool CheckDuplicateLayer(HiradAppModel hiradAppModel);
    }
}
