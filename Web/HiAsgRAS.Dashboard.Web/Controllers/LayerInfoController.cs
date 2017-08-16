using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;


namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class LayerInfoController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        ILayerInfoBLL _layerInfoBLL = null;

        public LayerInfoController(IUserDetailBLL usersBLL, ILayerInfoBLL layerInfoBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _layerInfoBLL = layerInfoBLL;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
          [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true, Location = System.Web.UI.OutputCacheLocation.None)] 
        public ActionResult GetLayerList()
        {

            var layerList = _layerInfoBLL.GetLayerList();      
            return Json(layerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLayerDetails(int lId)
        {

            var layerDetail = _layerInfoBLL.GetLayerDetails(lId);          
            return Json(layerDetail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateLayerList(LayerInfoModel layerInfoModel)
        {
            if (_layerInfoBLL.CheckDuplicateLayerName(layerInfoModel))
            {
                return Json(new { RecStatus = "Duplicate" });
            }
            if (layerInfoModel.Id > 0)
            {              
                layerInfoModel.ModifiedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                layerInfoModel.ModifiedDate = DateTime.Now;
            }
            else
            {
                layerInfoModel.CreatedBy = Session[ApplicationConstants.Constants.UserName].ToString();
                layerInfoModel.CreatedDate = DateTime.Now;
              
            }                     
            _layerInfoBLL.UpdateLayerInfo(layerInfoModel);
            return Json(new { RecStatus = "Saved" });
        }
        [HttpPost]
        public ActionResult DeleteLayer(int lId)
        {           
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                List<AppsListModel> appList;
                appList = _layerInfoBLL.GetAllAppsByLayerId(lId);
                if (appList.Count > 0)
                {
                    return Json(new { RecStatus = "Not Deleted" });
                }  
                _layerInfoBLL.DeleteLayer(lId, Session[ApplicationConstants.Constants.UserName].ToString());
                return Json(new { RecStatus = "Deleted" });
            }
            return Json(new { RecStatus = "You are not authorized to delete." });
        }


        /// <summary>
        /// This method is used to populate the Popup window to display the Layer information while Add, View, Edit
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="actionMode"></param>
        /// <returns></returns>
        public ActionResult ViewLayerInformation(int layerId, string actionMode)
        {

            LayerInfoModel objLayerInfoModel = new LayerInfoModel();
            if (!string.IsNullOrEmpty(actionMode) && actionMode.Trim() == "Add")
            {
                objLayerInfoModel.ActionMode = actionMode;
                objLayerInfoModel.NewLayerId = 0;

            }
            else
            {
                objLayerInfoModel = _layerInfoBLL.GetLayerDetails(layerId);
                if (objLayerInfoModel != null)
                {
                    objLayerInfoModel.ActionMode = actionMode;
                }

            }
            return PartialView(objLayerInfoModel);
        }

    }
}