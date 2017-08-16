using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradAppController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IHiradAppBLL _hiradAppBLL = null;
        ILayerInfoBLL _layerInfoBLL = null;
        IRenewalBLL _renewalBLL = null;
        IStatusTypeBLL _statusTypeBLL = null;
        IApplicationEntityBLL _applicationEntityBLL = null;
        public HiradAppController(
            IUserDetailBLL usersBLL, IHiradAppBLL hiradAppBLL,
            ILayerInfoBLL layerInfoBLL, IRenewalBLL renewalBLL, IStatusTypeBLL statusTypeBLL, IApplicationEntityBLL applicationEntityBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradAppBLL = hiradAppBLL;
            _layerInfoBLL = layerInfoBLL;
            _renewalBLL = renewalBLL;
            _statusTypeBLL = statusTypeBLL;
            _applicationEntityBLL = applicationEntityBLL;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchAppList(HiradAppModel hiradAppModel)
        {
            var result = _hiradAppBLL.searchHiradAppList_procedure(hiradAppModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// This method is used to populate the Popup window to display the server information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewClientAppsInformation(int appId, string actionMode)
        {
            int defaultId = 0;
            HiradAppModel objHiradAppModel = new HiradAppModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                defaultId = 1;
                objHiradAppModel.ActionMode = actionMode;
                objHiradAppModel.StatusTypeId = defaultId;
                objHiradAppModel.StatusTypeChangedOn = DateTime.Now;
                objHiradAppModel.NewAppId = 0;

            }
            else
            {
                objHiradAppModel = _hiradAppBLL.GetAppDetails(appId);
                if (objHiradAppModel != null)
                {
                    defaultId = objHiradAppModel.StatusTypeId ?? 1;
                    objHiradAppModel.ActionMode = actionMode;
                }

            }
            var lstStatusTypes = _statusTypeBLL.GetAll();
            objHiradAppModel.StatusTypes = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstStatusTypes, "Id", "StatusText", defaultId);

            var lstLayerTypes = _layerInfoBLL.GetAll();
            if (lstLayerTypes != null)
            {
                lstLayerTypes = lstLayerTypes.Where(x => x.IsDeleted == false).ToList();
                lstLayerTypes.Insert(0, new LayerInfoModel());
            }
            objHiradAppModel.ApplicationLayers = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstLayerTypes, "Id", "AppLayerName", 0);

            var lstAppEntities = _applicationEntityBLL.GetAll();

            objHiradAppModel.ApplicationEntities = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstAppEntities, "Name", "Name", defaultId);


            return PartialView(objHiradAppModel);
        }


        [HttpPost]
        public ActionResult GetAppDetails(int appId)
        {

            var appDetail = _hiradAppBLL.GetAppDetails(appId);
            return Json(appDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAppList(HiradAppModel hiradAppModel)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                if (_hiradAppBLL.CheckDuplicateApp(hiradAppModel))
                {
                    return Json(new { RecStatus = "Duplicate" });
                }
                if (!string.IsNullOrEmpty(hiradAppModel.ApplicationLayer) && !string.IsNullOrEmpty(hiradAppModel.Layer5Location))
                {
                    if (_hiradAppBLL.CheckDuplicateLayer(hiradAppModel))
                    {
                        return Json(new { RecStatus = "Duplicate Layer" });
                    }
                }
                if (hiradAppModel.Id > 0)
                {
                    hiradAppModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradAppModel.ModifiedDate = DateTime.Now;
                    if (hiradAppModel.StatusTypeId != hiradAppModel.PreviousStatusTypeId)
                    {
                        hiradAppModel.StatusTypeChangedOn = DateTime.Now;
                    }
                }
                else
                {
                    hiradAppModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradAppModel.CreatedDate = DateTime.Now;
                    hiradAppModel.HiradNew = true;
                    hiradAppModel.StatusTypeChangedOn = DateTime.Now;
                }

                _hiradAppBLL.UpdateApp(hiradAppModel);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }

        [HttpPost]
        public ActionResult DeleteApp(int appId)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _hiradAppBLL.DeleteApp(appId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to delete." });
        }

        [HttpGet]
        [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)]
        public ActionResult GetLayerList()
        {
            var layerList = _layerInfoBLL.GetLayerList();
            return Json(layerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateRenewal()
        {
            RenewalViewModel renewalModel = new RenewalViewModel();

            string renewalRequired = string.Empty;
            string OwnerPrimary = string.Empty;
            string MailPrimary = string.Empty;
            string OwnerSecondary = string.Empty;
            string MailSecondary = string.Empty;
            string ApplicationName = string.Empty;
            string RenewalDate = string.Empty;

            ViewBag.ApplicationName = Request["app"];
            ApplicationName = ViewBag.ApplicationName.ToString();
            renewalModel.ApplicationName = ApplicationName;
            ViewBag.OwnerSecondary = Request["OSec"];
            OwnerSecondary = ViewBag.OwnerSecondary.ToString();
            renewalModel.OwnerSecondary = OwnerSecondary;
            ViewBag.MailSecondary = Request["EmSec"];
            MailSecondary = ViewBag.MailSecondary.ToString();
            renewalModel.MailSecondary = MailSecondary;
            ViewBag.OwnerPrimary = Request["OPri"];
            OwnerPrimary = ViewBag.OwnerPrimary.ToString();
            renewalModel.OwnerPrimary = OwnerPrimary;
            ViewBag.RenewalDate = Request["reDate"];
            RenewalDate = ViewBag.RenewalDate.ToString();
            renewalModel.RenewalDate = RenewalDate;
            ViewBag.MailPrimary = Request["EmPri"];
            MailPrimary = ViewBag.MailPrimary.ToString();
            renewalModel.MailPrimary = MailPrimary;
            ViewBag.ResponseType = Request["response"];
            renewalRequired = ViewBag.ResponseType.ToString();

            ViewBag.ApplicationId = Request["appid"];
            int applicationId = Convert.ToInt16(ViewBag.ApplicationId);
            renewalModel.ApplicationId = applicationId;
            ViewBag.UniqueId = Request["uniqueid"];



            renewalModel.UniqueId = ViewBag.UniqueId;

            // var uniqueId = Convert.ToInt64(renewalModel.UniqueId);
            var record = _renewalBLL.GetAll().Where(r => r.UniqueId == renewalModel.UniqueId.ToUpper()).ToList();

            if (record[0].IsRenewalRequired == null)
            {
                if (renewalRequired == "Y")
                {
                    renewalModel.IsRenewalRequired = true;
                    _renewalBLL.UpdateRenewalRecord(renewalModel);
                    string mailBody = _renewalBLL.composeMailBody(renewalModel.ApplicationName, renewalModel.RenewalDate, renewalModel.OwnerPrimary, renewalModel.MailPrimary, renewalModel.OwnerSecondary, renewalModel.MailSecondary, renewalModel.ApplicationId, renewalModel.UniqueId);
                    _renewalBLL.SendHtmlFormattedEmail(renewalModel.MailSecondary, "Application Renewal Process - Reminder Mail", mailBody);
                }
                else
                {
                    renewalModel.IsRenewalRequired = false;
                    _renewalBLL.UpdateRenewalRecord(renewalModel);
                    Response.Write("Your response is recorded.");
                }
            }
            else
            {
                Response.Write("Response is already recorded");
            }
            return View();

        }
        public ActionResult SearchServerInformation()
        {
            return PartialView();
        }
        public ActionResult SearchBAOInformation()
        {
            return PartialView("SearchBAOInformation");
        }

    }
}
