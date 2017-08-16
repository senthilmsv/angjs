using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradDbMonitorController: ControllerBase
    {
          IUserDetailBLL _usersBLL = null;
        IHiradDbMonitorBLL _hiradDbMonitorBLL = null;
        IStatusTypeBLL _statusTypeBLL = null;
        public HiradDbMonitorController(IUserDetailBLL usersBLL, IHiradDbMonitorBLL hiradDbMonitorBLL, IStatusTypeBLL statusTypeBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradDbMonitorBLL = hiradDbMonitorBLL;
            _statusTypeBLL = statusTypeBLL;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDBServerList()
        {

            var serverList = _hiradDbMonitorBLL.GetDBServerAll();
            return Json(serverList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDBList()
        {
            var serverList = _hiradDbMonitorBLL.HiradDbList_procedure();
            return Json(serverList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to populate the Popup window to display the DB information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewDatabaseInformation(int dbId, string actionMode)
        {
            int defaultId = 0;
            HiradDbMonitorModel objHiradDbMonitorModel = new HiradDbMonitorModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                defaultId = 1;
                objHiradDbMonitorModel.ActionMode = actionMode;
                objHiradDbMonitorModel.StatusTypeId = defaultId;
                objHiradDbMonitorModel.StatusTypeChangedOn = DateTime.Now;
                objHiradDbMonitorModel.NewDbId = 0;
                

            }
            else
            {
                objHiradDbMonitorModel = _hiradDbMonitorBLL.GetDbDetails(dbId);
                if (objHiradDbMonitorModel != null)
                {
                    defaultId = objHiradDbMonitorModel.StatusTypeId ?? 1;
                    objHiradDbMonitorModel.ActionMode = actionMode;
                }

            }
            var lstStatusTypes = _statusTypeBLL.GetAll();
            objHiradDbMonitorModel.StatusTypes = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstStatusTypes, "Id", "StatusText", defaultId);

            return PartialView(objHiradDbMonitorModel);
        }

        [HttpPost]
        public ActionResult GetDbDetails(int appId)
        {

            var dbDetail = _hiradDbMonitorBLL.GetDbDetails(appId);
            return Json(dbDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateDbList(HiradDbMonitorModel hiradDbMonitorModel)
        {
            if (_hiradDbMonitorBLL.CheckDuplicateDatabase(hiradDbMonitorModel))
            {
                return Json(new { RecStatus = "Duplicate" });
            }
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                if (hiradDbMonitorModel.Id > 0)
                {
                    hiradDbMonitorModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradDbMonitorModel.ModifiedDate = DateTime.Now;

                    if (hiradDbMonitorModel.StatusTypeId != hiradDbMonitorModel.PreviousStatusTypeId)
                    {
                        hiradDbMonitorModel.StatusTypeChangedOn = DateTime.Now;
                    }
                }
                else
                {
                    hiradDbMonitorModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradDbMonitorModel.CreatedDate = DateTime.Now;
                    hiradDbMonitorModel.StatusTypeChangedOn = DateTime.Now;
                }
             
                _hiradDbMonitorBLL.UpdateDb(hiradDbMonitorModel);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }

        [HttpPost]
        public ActionResult DeleteDb(int dbId)
        {

            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _hiradDbMonitorBLL.DeleteDb(dbId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }
        public ActionResult SearchServerInformation()
        {
            return PartialView("SearchDbServerInformation");
        }
    }
}