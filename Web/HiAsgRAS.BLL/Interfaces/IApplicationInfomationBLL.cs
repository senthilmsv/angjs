using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiAsgRAS.ViewModel;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IApplicationInfomationBLL : IBaseBLL<ApplicationInformationModel>
    {
        int AddApplicationInformation(ApplicationInformationModel appInfoModel);
    }
}
