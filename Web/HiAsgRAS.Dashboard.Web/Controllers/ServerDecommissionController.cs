using HiAsgRAS.BLL.Interfaces;
using HiAsgRAS.Common;
using HiAsgRAS.ViewModel;
using System.Linq;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class ServerDecommissionController : ControllerBase
    {
        IUserDetailBLL _usersBLL = null;
        IServerDecommissionBLL _serverDecommissionBLL = null;
        IStatusTypeBLL _statusTypeBLL = null;
        IHiradServerBLL _serverBLL = null;

        public ServerDecommissionController(IUserDetailBLL usersBLL,
            IServerDecommissionBLL serverDecommissionBLL,
            IStatusTypeBLL statusTypeBLL,
            IHiradServerBLL serverBLL)
            : base(usersBLL)
        {
            _usersBLL = usersBLL;
            _serverDecommissionBLL = serverDecommissionBLL;
            _statusTypeBLL = statusTypeBLL;
            _serverBLL = serverBLL;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult GetAllServers()
        {
            var rows = _serverDecommissionBLL.GetAllServers()
                         .Select(x => new HiradServerModel
                         {
                             Id = x.Id,
                             SystemName = x.SystemName
                         }).ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllStatusType()
        {
            var lstStatusTypes = _statusTypeBLL.GetAll();
            return Json(lstStatusTypes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServerStatus(int serverId)
        {
            var row = _serverDecommissionBLL.GetAllServers().Where(x => x.Id.Equals(serverId)).FirstOrDefault();          
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppsCountByServer(int serverId)
        {
            var recCount = 0;
            recCount = _serverBLL.GetAllAppsByServerId(serverId).Where(x => x.StatusTypeId.Equals(1) || x.StatusTypeId.Equals(3)).Count();

            return Json(recCount, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllClientAppsList(int Id)
        {
            var clientAppsList = _serverDecommissionBLL.GetAllClientAppsList(Id);
            return Json(clientAppsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllWebList(int Id)
        {
            var webAppsList = _serverDecommissionBLL.GetAllWebList(Id);
            return Json(webAppsList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDBServerList(int Id)
        {
            var dbServerList = _serverDecommissionBLL.GetDBServerList(Id);
            return Json(dbServerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveServerInfo(string Ids, string source, int newServerId, int oldServerId)
        {
            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _serverDecommissionBLL.SaveServerInfo(Ids, source, newServerId, oldServerId);
                return Json(new { RecStatus = "Saved" });
            }
            return Json(new { RecStatus = "You are not authorized to Update." });
        }

        [HttpPost]
        public ActionResult UpdateServerStatus(int serverId, int statusTypeId)
        {           

            if (Session[ApplicationConstants.Constants.UserType].Equals(ApplicationConstants.UserType.Admin))
            {
                _serverBLL.UpdateServerStatus(serverId, statusTypeId);
                return Json(new { RecStatus = "Updated" });
            }
            return Json(new { RecStatus = "You are not authorized to Update." });
        }
    }
}