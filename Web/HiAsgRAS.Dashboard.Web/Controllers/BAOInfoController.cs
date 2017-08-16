using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Configuration;
using System.IO;
namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class BAOInfoController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IBAOInfoBLL _iBAOInfoBLL = null;
        IApplicationInfomationBLL _iApplicationInfomationBLL = null;
        public BAOInfoController(IUserDetailBLL usersBLL, IBAOInfoBLL iBAOInfoBLL, IApplicationInfomationBLL iApplicationInfomationBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _iBAOInfoBLL = iBAOInfoBLL;
            _iApplicationInfomationBLL = iApplicationInfomationBLL;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchBAOList(BAOInfoModel baoInfoModel)
        {
            var result = _iBAOInfoBLL.searchBAOList_procedure(baoInfoModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBAOList()
        {

            var baoList = _iBAOInfoBLL.GetBAOList();            
            return Json(baoList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetBAODetails(int baoId)
        {

            var baoDetail = _iBAOInfoBLL.GetBAODetails(baoId);         
            return Json(baoDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateBAOList(BAOInfoModel baoInfoModel)
        {
            if (_iBAOInfoBLL.CheckDuplicateBAO(baoInfoModel))
            {
                return Json(new { RecStatus = "Duplicate" });
            }
           
            if (baoInfoModel.Id>0)
            {               
                baoInfoModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                baoInfoModel.ModifiedDate = DateTime.Now;
            }
            else
            {
                baoInfoModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString(); 
                baoInfoModel.CreatedDate = DateTime.Now;
               
            }           
           
            _iBAOInfoBLL.UpdateBAOInfo(baoInfoModel);
            return Json(new { RecStatus = "Saved" });
        }
        [HttpPost]
        public ActionResult DeleteBAO(int baoId)
        {
          
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                List<AppsListModel> appList;
                appList = _iBAOInfoBLL.GetAllAppsByBAOId(baoId);
                if (appList.Count > 0)
                {
                    return Json(new { RecStatus = "Not Deleted" });
                }  
                _iBAOInfoBLL.DeleteBAO(baoId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to delete." });
        }

        /// <summary>
        /// This method is used to populate the Popup window to display the BAO information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewBAOInformation(int baoId, string actionMode)
        {
            
            BAOInfoModel objBAOInfoModel = new BAOInfoModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {                
                objBAOInfoModel.ActionMode = actionMode;
                objBAOInfoModel.NewBAOId = 0;

            }
            else
            {
                objBAOInfoModel = _iBAOInfoBLL.GetBAODetails(baoId);
                if (objBAOInfoModel != null)
                {
                    objBAOInfoModel.ActionMode = actionMode;
                }

            }
            return PartialView(objBAOInfoModel);
        }

        public ActionResult UpdateApplicationBAOInformation(int appId, string appType)
        {
            BAOInfoModel objBAOInfoModel = new BAOInfoModel();

            objBAOInfoModel = _iBAOInfoBLL.GetBaoInfoByApplication(appId, appType);
            return View(objBAOInfoModel);
        }

        [HttpPost]
        public ActionResult UpdateApplicationBAOInformation(BAOInfoModel baoInfoModel)
        {

            if (_iBAOInfoBLL.CheckDuplicateBAO(baoInfoModel))
            {
                return Json(new { RecStatus = "Duplicate" });
            }

            baoInfoModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
            baoInfoModel.ModifiedDate = DateTime.Now;

            _iBAOInfoBLL.UpdateBAOInfo(baoInfoModel);

            if (baoInfoModel.ApplicationInformation.ApplicationInformation.ToUpper().Equals("NO CHANGE"))
            {
                return Json(new { RecStatus = "Saved" });
            }
            ApplicationInformationModel appInfoModel = new ApplicationInformationModel();
            appInfoModel.ApplicationId = baoInfoModel.ApplicationInformation.ApplicationId;
            appInfoModel.ApplicationName = baoInfoModel.ApplicationInformation.ApplicationName;
            appInfoModel.ApplicationInformation = baoInfoModel.ApplicationInformation.ApplicationInformation;
            appInfoModel.ApplicationType = baoInfoModel.ApplicationInformation.ApplicationType;
            appInfoModel.Comments = baoInfoModel.ApplicationInformation.Comments;

            appInfoModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
            appInfoModel.CreatedDate = DateTime.Now;

            _iApplicationInfomationBLL.AddApplicationInformation(appInfoModel);

            MailHelper mailhelper = new MailHelper();
            mailhelper.Subject = ConfigurationManager.AppSettings["MailSubject"];
            mailhelper.Sender = ConfigurationManager.AppSettings["MailFrom"];
            mailhelper.Recipient = ConfigurationManager.AppSettings["MailTo-Admin"];
            mailhelper.Body = composeMailBody(appInfoModel.ApplicationName, appInfoModel.ApplicationInformation, appInfoModel.Comments);
            mailhelper.Send();

            return Json(new { RecStatus = "Saved" });
        }

        public string composeMailBody(string appname, string appInformtion, string comments)
        {
            string body = GetTemplate();
            body = body.Replace("{ApplicationName}", appname);
            body = body.Replace("{ApplicationInformation}", appInformtion);
            body = body.Replace("{Comments}", comments);

            return body;
        }


        public string GetTemplate()
        {

            string body = string.Empty;

            string pathfortemplate = string.Empty;

            pathfortemplate = AppDomain.CurrentDomain.BaseDirectory + "\\Templates\\ApplicationInformation.html";

            using (StreamReader reader = new StreamReader(pathfortemplate))
            {
                body = reader.ReadToEnd();
            }

            return body;

        }
    }
}
