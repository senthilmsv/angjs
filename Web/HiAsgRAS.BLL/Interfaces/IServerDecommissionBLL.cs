using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
   public interface IServerDecommissionBLL: IBaseBLL<HiradServerModel>
    {
       IEnumerable<HiradServerModel> GetAllServers();
       IEnumerable<HiradAppModel> GetAllClientAppsList(int Id);
       IEnumerable<HiradWebModel> GetAllWebList(int Id);
       IEnumerable<HiradDbMonitorModel> GetDBServerList(int Id);
       bool SaveServerInfo(string Ids, string source, int newServerId, int oldServerId);


    }
}
