using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class ApplicationInformationController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IApplicationInfomationBLL _iAPPInfoBLL = null;
        public ApplicationInformationController(IUserDetailBLL usersBLL, IApplicationInfomationBLL iAPPInfoBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _iAPPInfoBLL = iAPPInfoBLL;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetApplicationInfoList()
        {
            var result = _iAPPInfoBLL.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAppInformation(int appId)
        {
            ApplicationInformationModel objAppInfoModel = new ApplicationInformationModel();
            objAppInfoModel = _iAPPInfoBLL.GetById(appId);            
            return PartialView(objAppInfoModel);
        }

        [HttpPost]
        public ActionResult AddApplicationInformation(ApplicationInformationModel appInformationModel)
        {
            appInformationModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
            appInformationModel.CreatedDate = DateTime.Now;
            _iAPPInfoBLL.AddApplicationInformation(appInformationModel);
            return Json(new { RecStatus = "Saved" });
        }
    }
}