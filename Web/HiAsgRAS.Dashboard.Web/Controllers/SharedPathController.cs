using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class SharedPathController : ControllerBase
    {

        IUserDetailBLL _usersBLL = null;
        ISharedPathBLL _objBLL = null;
        IHiradServerBLL _serverBLL = null;
        public SharedPathController(IUserDetailBLL usersBLL, ISharedPathBLL BLL, IHiradServerBLL serverBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _objBLL = BLL;
            _serverBLL = serverBLL;
        }


        //
        // GET: /SharedPath/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllSharedPathList()
        {
            var pathsList = _objBLL.GetSharedNWPathDetails();
            return Json(pathsList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSharedPathList(SharedPathModel sharedPathModel)
        {

            if (sharedPathModel.Id > 0)
            {
                sharedPathModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                sharedPathModel.ModifiedDate = DateTime.Now;
            }
            else
            {
                sharedPathModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                sharedPathModel.CreatedDate = DateTime.Now;

            }
            _objBLL.UpdateSharedPathList(sharedPathModel);
            return Json(new { RecStatus = "Saved" });
        }

        [HttpPost]
        public ActionResult DeleteSharedPath(int sharedPathId)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _objBLL.DeleteSharedPath(sharedPathId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to delete." });
        }


        /// <summary>
        /// This method is used to populate the Popup window to display the Shared NW Path information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewSharedPathInformation(int sharedPathId, string actionMode)
        {
            int defaultId = -1;
            SharedPathModel objSharedPathModel = new SharedPathModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                objSharedPathModel.ActionMode = actionMode;
            }
            else
            {
                objSharedPathModel = _objBLL.GetSharedNWPathDetailsById(sharedPathId);
                if (objSharedPathModel != null)
                {
                    objSharedPathModel.ActionMode = actionMode;
                }

            }

            var appServers = _serverBLL.GetAllMonitoringServers().Select(x => new HiradServerModel
                                    {
                                        Id = x.Id,
                                        SystemName = x.SystemName
                                    }).ToList();
            if (appServers != null)
            {
                appServers.Insert(0, new HiradServerModel());
            }
            objSharedPathModel.AppServers = HiAsgRAS.Dashboard.Web.Common.CommonWeb.CommonUtilities.GetSelectListItem(
                                                appServers, "Id", "SystemName", defaultId);

            return PartialView(objSharedPathModel);
        }

    }
}