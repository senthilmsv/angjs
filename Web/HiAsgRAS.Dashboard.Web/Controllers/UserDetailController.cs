using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class UserDetailController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;

        public UserDetailController(IUserDetailBLL usersBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;            
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]          
        public ActionResult GetUserList()
        {
            var layerList = _usersBLL.GetUserList();          
            return Json(layerList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetUserDetails(int uId)
        {

            var userDetail = _usersBLL.GetUserDetails(uId);
           
            return Json(userDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUserList(UserDetailModel userDetailModel)
        {
            if (_usersBLL.CheckDuplicateUserNUID(userDetailModel))
            {
                return Json(new { RecStatus = "DuplicateNUID" });
            }
            if (_usersBLL.CheckDuplicateUserEmail(userDetailModel))
            {
                return Json(new { RecStatus = "DuplicateEmail" });
            }

            if (userDetailModel.Id > 0)
            {
                userDetailModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                userDetailModel.ModifiedDate = DateTime.Now;
            }
            else
            {
                userDetailModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                userDetailModel.CreatedDate = DateTime.Now;
               
            }

            _usersBLL.UpdateUserDetail(userDetailModel);
            return Json(new { RecStatus = "Saved" });
        }
        [HttpPost]
        public ActionResult DeleteUser(int uId)
        {
         
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _usersBLL.DeleteUser(uId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to delete." });
        }


        /// <summary>
        /// This method is used to populate the Popup window to display the User information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewUserInformation(int userId, string actionMode)
        {

            UserDetailModel objUserDetailModel = new UserDetailModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                objUserDetailModel.ActionMode = actionMode;
                objUserDetailModel.NewUserId = 0;

            }
            else
            {
                objUserDetailModel = _usersBLL.GetUserDetails(userId);
                if (objUserDetailModel != null)
                {
                    objUserDetailModel.ActionMode = actionMode;
                }

            }
            return PartialView(objUserDetailModel);
        }        
    }
}
