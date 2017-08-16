using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateApplicationInformation()
        {
            int id;
            string type;
            id = Convert.ToInt16(Request["appId"]);
            type = Convert.ToString(Request["appType"]);
            return RedirectToAction("`", "BAOInfo", new { appId = id, appType = type });
        }
    }
}