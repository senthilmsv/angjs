using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiAsgRAS.BLL.Interfaces
{
    public interface IRenewalBLL : IBaseBLL<RenewalViewModel>
    {
        void UpdateRenewalRecord(RenewalViewModel renewalDetailModel);
        string composeMailBody(string appname, string renewalDate, string ownerPrimary, string mailPrimary, string ownerSecondary, string mailSecondary, int applicationId, string uniqueId);
        string PopulateBody();
        void SendHtmlFormattedEmail(string recepientEmail, string subject, string body);
    }
}
