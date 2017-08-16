using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class MonitoringConfigController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradServerBLL _hiradServerBLL = null;

        public MonitoringConfigController(IUserDetailBLL usersBLL, IHiradServerBLL hiradServerBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerBLL = hiradServerBLL;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdateMonitorInfo(string Ids, string source)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {

                _hiradServerBLL.UpdateMonitorInfo(Ids, source);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }
    }
}