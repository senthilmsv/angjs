using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradDbMonitorLogController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradDbMonitorBLL _hiradDbMonitorBLL = null;


        public HiradDbMonitorLogController(IUserDetailBLL usersBLL,
            IHiradDbMonitorBLL hiradDbMonitorBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradDbMonitorBLL = hiradDbMonitorBLL;
        }

        public JsonResult GetAllDBLogStatusByLastRun()
        {
            List<DbMonitorLogStatusByLastRunModel> lstLastRunStatus =
                                                _hiradDbMonitorBLL.GetAllDBLogStatusByLastRun();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }


        //Partial View to Load popup data
        public ActionResult ViewDBLog(int DbMonitorId)
        {
            var recs = _hiradDbMonitorBLL.GetAllDBMonitorLogsByDB(DbMonitorId);
            return PartialView(recs);

        }
        
    }
}