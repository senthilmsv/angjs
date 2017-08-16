using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradWebLogController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradWebBLL _hiradWebBLL = null;

        public HiradWebLogController(IUserDetailBLL usersBLL,
            IHiradWebBLL hiradWebBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradWebBLL = hiradWebBLL;
        }

        public JsonResult GetAllWebLogStatusByLastRun()
        {
            List<WebsiteLogStatusByLastRunModel> lstLastRunStatus = 
                                                _hiradWebBLL.GetAllWebLogStatusByLastRun();
            return Json(lstLastRunStatus, JsonRequestBehavior.AllowGet);
        }
        

        //Partial View to Load popup data
        public ActionResult ViewWebLog(int webId)
        {
            var recs = _hiradWebBLL.GetAllWebLogsByWebsite(webId);
            return PartialView(recs);
            
        }
        public JsonResult GetAllSPSites()
        {
            var data = _hiradWebBLL.GetAllSPSites();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}