using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;


namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradWebController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradWebBLL _hiradWebBLL = null;
        IStatusTypeBLL _statusTypeBLL = null;

        public HiradWebController(IUserDetailBLL usersBLL, IHiradWebBLL hiradWebBLL, IStatusTypeBLL statusTypeBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradWebBLL = hiradWebBLL;
            _statusTypeBLL = statusTypeBLL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WebApplication(string webSiteType)
        {
            Session["WebSiteType"] = webSiteType;
            return View("Index");
        }

        public ActionResult SPApplication(string webSiteType)
        {
            Session["WebSiteType"] = webSiteType;
            return View("Index");
        }

        public ActionResult GetAllWebList()
        {

            var serverList = _hiradWebBLL.GetWebAll();
            return Json(serverList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchWebAppList(HiradWebModel hiradWebModel)
        {
            hiradWebModel.WebSiteType = Session["WebSiteType"].ToString();
            var result = _hiradWebBLL.searchHiradWebAppList_procedure(hiradWebModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllWebAppsList(HiradWebModel hiradWebModel)
        {            
            var result = _hiradWebBLL.searchHiradWebAppList_procedure(hiradWebModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to populate the Popup window to display the Web information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewWebAppInformation(int webAppId, string actionMode)
        {
            int defaultId = 0;
            HiradWebModel objHiradWebModel = new HiradWebModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                defaultId = 1;
                objHiradWebModel.ActionMode = actionMode;
                objHiradWebModel.StatusTypeId = defaultId;
                objHiradWebModel.StatusTypeChangedOn = DateTime.Now;
                objHiradWebModel.NewWebAppId = 0;
                objHiradWebModel.WebSiteType = Session["WebSiteType"].ToString();

            }
            else
            {
                objHiradWebModel = _hiradWebBLL.GetWebAppDetails(webAppId);
                if (objHiradWebModel != null)
                {
                    defaultId = objHiradWebModel.StatusTypeId ?? 1;
                    objHiradWebModel.ActionMode = actionMode;
                }

            }
            var lstStatusTypes = _statusTypeBLL.GetAll();
            objHiradWebModel.StatusTypes = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstStatusTypes, "Id", "StatusText", defaultId);

            return PartialView(objHiradWebModel);
        }

        [HttpPost]
        public ActionResult GetWebAppDetails(int appId)
        {

            var appDetail = _hiradWebBLL.GetWebAppDetails(appId);
            return Json(appDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateWebAppList(HiradWebModel hiradWebModel)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                if (_hiradWebBLL.CheckDuplicateWebApp(hiradWebModel))
                {
                    return Json(new { RecStatus = "Duplicate" });
                }
                if (hiradWebModel.Id > 0)
                {
                    hiradWebModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradWebModel.ModifiedDate = DateTime.Now;

                    if (hiradWebModel.StatusTypeId != hiradWebModel.PreviousStatusTypeId)
                    {
                        hiradWebModel.StatusTypeChangedOn = DateTime.Now;
                    }
                }
                else
                {
                    hiradWebModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradWebModel.CreatedDate = DateTime.Now;
                    hiradWebModel.HiradNew = true;
                    hiradWebModel.WebSiteType = Session["WebSiteType"].ToString();

                    hiradWebModel.StatusTypeChangedOn = DateTime.Now;
                }

                //Application Renewal == No, set Null value to renewal date
                if (hiradWebModel.IsRenewal.HasValue && hiradWebModel.IsRenewal == false)
                {
                    hiradWebModel.ApplicationRenewalDate = null;
                }
                _hiradWebBLL.UpdateWebApp(hiradWebModel);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }

        [HttpPost]
        public ActionResult DeleteWebApp(int appId)
        {

            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _hiradWebBLL.DeleteWebApp(appId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }
        public ActionResult SearchServerInformation()
        {
            return PartialView();
        }
        public ActionResult SearchBAOInformation()
        {
            return PartialView();
        }
    }
}
