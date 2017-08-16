using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.Dashboard.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    //[OutputCache(VaryByParam = "*", Duration = 0, NoStore = true, Location = System.Web.UI.OutputCacheLocation.ServerAndClient)]    
    //[OutputCache(VaryByParam = "*", Duration = 10, Location = System.Web.UI.OutputCacheLocation.Client)]    
    public abstract class ControllerBase : Controller
    {
        IUserDetailBLL _userDetailBLL = null;

        public ControllerBase(IUserDetailBLL objUserBLL)
         {
             _userDetailBLL = objUserBLL;
         }
       
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJSONResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            /* take current action*/
            var action = (string)filterContext.RouteData.Values["action"];

            if (Session[ApplicationConstants.Constants.LoggedInUser] == null)
            {
                var userNUID = Request.LogonUserIdentity.Name;

                int indexOf = userNUID.IndexOf("\\");
                if (indexOf > 0)
                {
                    userNUID = userNUID.Substring(indexOf + 1);
                }

                if (ConfigurationManager.AppSettings[ApplicationConstants.AppKey.CustomUser] != null)
                {
                    string customUser = string.Empty;
                    customUser = ConfigurationManager.AppSettings[ApplicationConstants.AppKey.CustomUser];
                    if (!String.IsNullOrEmpty(customUser))
                    {
                        userNUID = customUser;
                    }
                }

                if (!string.IsNullOrEmpty(userNUID) && userNUID.Length > 12)
                {
                    userNUID = userNUID.Substring(0, 12);
                }

                Session[ApplicationConstants.Constants.UserName] = string.Empty;
                Session[ApplicationConstants.Constants.UserType] = string.Empty;
                var loginModel = _userDetailBLL.GetUserByNUID(userNUID);

                //if (loginModel == null)
                //{
                //    string errMessage = "Authentication Failed. User : " + userNUID + " does not have access.";
                //    throw new Exception(errMessage);
                //}
                //else 
                if (loginModel != null && (loginModel.IsActive.ToString().ToUpper().Trim() == "YES" 
                        || loginModel.IsActive.ToString().ToUpper().Trim() == "Y"))
                {
                    Session[ApplicationConstants.Constants.LoggedInUser] = loginModel;
                    Session[ApplicationConstants.Constants.UserName] = loginModel.UserName;
                    Session[ApplicationConstants.Constants.UserType] = loginModel.UserType;
                    Session[ApplicationConstants.Constants.UserNUID] = userNUID;
                    ViewBag.LoginUserModel = loginModel;
                }
                else
                {
                    //string errMessage = "Authentication Failed. User : " + userNUID + " does not have access.";
                    //throw new Exception(errMessage);
                    Session[ApplicationConstants.Constants.LoggedInUser] = "G";
                    Session[ApplicationConstants.Constants.UserName] = "Guest";
                    Session[ApplicationConstants.Constants.UserType] = ApplicationConstants.UserType.GeneralUser;
                    Session[ApplicationConstants.Constants.UserNUID] = "G";
                    //ViewBag.LoginUserModel = loginModel;
                }
            }
            
            CheckAcces(filterContext);          
            
        }

        private void CheckAcces(ActionExecutingContext filterContext)
        {
            var userType = Session[ApplicationConstants.Constants.UserType];
            var userName = Session[ApplicationConstants.Constants.UserName];
            var controllerName = (string)filterContext.RouteData.Values["controller"];

            if (userType.Equals(HiAsgRAS.Common.ApplicationConstants.UserType.GeneralUser))
            {                
                if (controllerName.ToUpper() == "BAOINFO" || controllerName.ToUpper() == "USERDETAIL" 
                    || controllerName.ToUpper() == "MONITORINGCONFIG")
                {                    
                    string errMessage = "Access Denied. User : " +
                                        Session[ApplicationConstants.Constants.UserNUID].ToString() +
                                        " does not have admin access.";
                    throw new Exception(errMessage);                    
                }
            }
        }
        
    }
}