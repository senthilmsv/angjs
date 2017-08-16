using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class DashboardController : ControllerBase
    {

        IUserDetailBLL _usersBLL = null;
        IHiradServerBLL _hiradServerBLL = null;
        IHiradServerLogBLL _hiradServerLogBLL = null;

        public DashboardController(IUserDetailBLL usersBLL,
            IHiradServerBLL hiradServerBLL,
            IHiradServerLogBLL hiServerLogBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerBLL = hiradServerBLL;
            _hiradServerLogBLL = hiServerLogBLL;
        }

        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(HiAsgRAS.Common.ApplicationConstants.UserType.GeneralUser))
            {
                return RedirectToAction("Index", "HiradServer");
            }

            ViewBag.RamThreshold = ApplicationConstants.GetRamThreshold();
            ViewBag.HddThreshold = ApplicationConstants.GetHddThreshold();
            ViewBag.RebootResetDays = ApplicationConstants.GetRebootReset();
            return View();
        }

        ////Partial View to Load popup data
        //public ActionResult ViewServerLog()
        //{
        //    return PartialView();
        //}

        //Partial View for SharePoint Sites
        public ActionResult ViewPortlet_SharePointSites()
        {
            return PartialView();
        }

        //Server Monitor Section
        public ActionResult ViewPortlet_Monitoring()
        {
            return PartialView();
        }

        //Site Visior
        public ActionResult ViewPortlet_SiteVisitors()
        {
            return PartialView();
        }

        //RAM Usage
        public ActionResult ViewPortlet_RAMUsage()
        {
            return PartialView();
        }

        //Daily Status
        public ActionResult ViewPortlet_DailyStatus()
        {
            //Collect Daily Status informations
            return PartialView();
        }

        //Site Visiotrs Count
        public ActionResult ViewPortlet_SiteVisitorsCount()
        {
            return PartialView();
        }

        //Reboot Count
        public ActionResult ViewPortlet_RebootCount()
        {
            return PartialView();
        }

        //HDD Count
        public ActionResult ViewPortlet_HDDCount()
        {
            return PartialView();
        }

        //RAM Count
        public ActionResult ViewPortlet_RAMCount()
        {
            return PartialView();
        }

        //ServerStatus Count
        public ActionResult ViewPortlet_ServerStatusCount()
        {
            return PartialView();
        }

        public JsonResult GetAllMonitoringServers()
        {
            var rows = _hiradServerBLL.GetAllMonitoringServers()
                        .Select(x => new HiradServerModel
                        {
                            Id = x.Id,
                            SystemName = x.SystemName
                        }).ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewPortlet_WebBlog()
        {
            return PartialView();
        }
    }
}