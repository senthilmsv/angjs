using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using HiAsgRAS.Dashboard.Web.Common;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HiradServerController : ControllerBase
    {
        //
        // GET: /HiradServer/

        IUserDetailBLL _usersBLL = null;
        IHiradServerBLL _hiradServerBLL = null;
        IStatusTypeBLL _statusTypeBLL = null;

        public HiradServerController(IUserDetailBLL usersBLL, 
            IHiradServerBLL hiradServerBLL,
            IStatusTypeBLL statusTypeBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _hiradServerBLL = hiradServerBLL;
            _statusTypeBLL = statusTypeBLL;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllServerList()
        {

            var serverList = _hiradServerBLL.GetServerAll();
            return Json(serverList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchServerList(HiradServerModel hiradSeverModel)
        {
            var result = _hiradServerBLL.searchHiradServerList_procedure(hiradSeverModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSystemInformation(string systemName)
        {
            if (!string.IsNullOrEmpty(systemName))
            {
                var result = _hiradServerBLL.GetSystemInformation(systemName);
                if (!string.IsNullOrEmpty(result.ErrorInfo))
                {
                    CustomCommonMethods.LogException("System Name :" + systemName
                        , Session[ApplicationConstants.Constants.UserName].ToString()
                        , result.Ex);
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { RecStatus = "Invalid SystemName" }); 
            }
        }

        /// <summary>
        /// This method is used to populate the Popup window to display the server information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewServerInformation(int serverId, string actionMode)
        {            
            int defaultId = 0;
            HiradServerModel objHiradServerModel = new HiradServerModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                defaultId = 1;
                objHiradServerModel.ActionMode = actionMode;
                objHiradServerModel.StatusTypeId = defaultId;
                objHiradServerModel.StatusTypeChangedOn = DateTime.Now;
                objHiradServerModel.NewServerId = 0;
                
            }
            else
            {
                objHiradServerModel = _hiradServerBLL.GetServerDetails(serverId);
                if (objHiradServerModel != null)
                {
                    defaultId = objHiradServerModel.StatusTypeId ?? 1;
                    objHiradServerModel.ActionMode = actionMode;
                }
                
            }
            var lstStatusTypes = _statusTypeBLL.GetAll();
            if (lstStatusTypes != null)
            {
                lstStatusTypes = lstStatusTypes.Where(x => x.StatusText.Contains("DECOMM").Equals(false)).ToList();
            }
            objHiradServerModel.StatusTypes = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                lstStatusTypes, "Id", "StatusText", defaultId);
            
            return PartialView(objHiradServerModel);
        }


        [HttpPost]
        public ActionResult GetServerDetails(int serverId)
        {
            var serverDetail = _hiradServerBLL.GetServerDetails(serverId);
            return Json(serverDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateServerList(HiradServerModel hiradServerModel)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                if (_hiradServerBLL.CheckDuplicateSystemName(hiradServerModel))
                {
                    return Json(new { RecStatus = "Duplicate" });
                }
                if (hiradServerModel.Id > 0)
                {
                    hiradServerModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradServerModel.ModifiedDate = DateTime.Now;
                    //hiradServerModel.HiradNew = Convert.ToBoolean(Session["HiradNew"]);

                    if (hiradServerModel.StatusTypeId != hiradServerModel.PreviousStatusTypeId)
                    {
                        hiradServerModel.StatusTypeChangedOn = DateTime.Now;
                    }
                    
                }
                else
                {
                    hiradServerModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                    hiradServerModel.CreatedDate = DateTime.Now;
                    //hiradServerModel.HiradNew = true;
                    hiradServerModel.StatusTypeChangedOn = DateTime.Now;                    
                }               

                _hiradServerBLL.UpdateServer(hiradServerModel);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }

        [HttpPost]
        public ActionResult DeleteServer(int serverId)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                List<AppsListModel> appList;
                appList = _hiradServerBLL.GetAllAppsByServerId(serverId);
                if (appList.Count>0)
                {
                    return Json(new { RecStatus = "Not Deleted" });
                }                
                _hiradServerBLL.DeleteServer(serverId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
        }

        public ActionResult UpdateAllSystemInformation()
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _hiradServerBLL.UpdateAllSystemInformation();
                return Json(new { RecStatus = "Updated" });
            }
            return Json(new { RecStatus = "You are not authorized to Save/Update." });
            
        }

    }
}
