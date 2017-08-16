using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.Dashboard.Web.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradServerHddController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradServerLogBLL _hiradServerLogBLL = null;
        public HiradServerHddController(IUserDetailBLL usersBLL, 
            IHiradServerLogBLL hiServerLogBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerLogBLL = hiServerLogBLL;
        }

        //
        // GET: /HiradServerHdd/
        public JsonResult GetCriticalHddCount()
        {
            DashboardTopRowModel objModel = new DashboardTopRowModel();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllHDDPercentage();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int thresold = ApplicationConstants.GetHddThreshold();
                objModel.RAMCount = lstLastRunStatus.FindAll(p => p.HddPercentage > thresold).Count();
                objModel.LastMonitoredAt = CommonWeb.CommonUtilities.TimeAgo(lstLastRunStatus[0].LoggedAt.Value);
            }
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllCriticalHdd()
        {
            List<LogStatusByLastRunModel> lstCriticalHdd = new List<LogStatusByLastRunModel>();
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllHDDPercentage();
            if (lstLastRunStatus != null && lstLastRunStatus.Any())
            {
                int thresold = ApplicationConstants.GetHddThreshold();
                lstCriticalHdd = lstLastRunStatus.FindAll(p => p.HddPercentage > thresold);
            }
            return PartialView(lstCriticalHdd);
        }

        public JsonResult GetAllHddStatusByLastRun()
        {
            List<LogStatusByLastRunModel> lstLastRunStatus = _hiradServerLogBLL.GetAllHDDPercentage();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }
	}
}