using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradServerRebController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradServerLogBLL _hiradServerLogBLL = null;
        public HiradServerRebController(IUserDetailBLL usersBLL, 
            IHiradServerLogBLL hiServerLogBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerLogBLL = hiServerLogBLL;
        }

        //
        // GET: /HiradServerHdd/
        public JsonResult GetCriticalRebCount()
        {
            DashboardTopRowModel objModel = new DashboardTopRowModel();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRebootServers();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int RebootResetDays = HiAsgRAS.Common.ApplicationConstants.GetRebootReset();
                objModel.RebootResetDays = lstLastRunStatus.FindAll(p => p.LastBootInDays >= RebootResetDays).Count();
                objModel.LastMonitoredAt = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.TimeAgo(
                                                lstLastRunStatus[0].LoggedAt.Value);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCriticalReb()
        {
            List<LogStatusByLastRunModel> lstCriticalReboot = new List<LogStatusByLastRunModel>();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRebootServers();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int RebootResetDays = ApplicationConstants.GetRebootReset();
                lstCriticalReboot = lstLastRunStatus.FindAll(p => p.LastBootInDays >= RebootResetDays);
            }
            return PartialView(lstCriticalReboot);
        }

        public JsonResult GetAllRebStatusByLastRun()
        {
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllRebootServers();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }
	}
}