using log4net;
using HiAsgRAS.Dashboard.Web.Common;
using HiAsgRAS.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using HiAsgRAS.Common;


namespace HiAsgRAS.Dashboard.Web.Filters
{
    /*
     * 1. Create log4net.Config file
     * 2. Update the connection string in the log config file 
     * <connectionString value="data source=[database server]; initial catalog=[database name];integrated security=false; persist security info=True;User ID=[user];Password=[password]" />
     * 3. Add the below line in AssemblyInfo.cs file 
     * [assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
     * 4. Create a Filters Folder and add this File
     * 5. Create the table, 
     * 
CREATE TABLE [dbo].[ExceptionLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[User] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](2000) NOT NULL,
	[Exception] [varchar](4000) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
          
     * Ref URL: "http://www.codeproject.com/Articles/140911/log4net-Tutorial"
     * */
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly ILog _logger;

        public CustomHandleErrorAttribute()
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);            
        }       

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            string controllerName = string.Empty, actionName = string.Empty;
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = filterContext.Exception.Message
                    }
                };
            }
            else
            {
                controllerName = (string)filterContext.RouteData.Values["controller"];
                actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.Result = new ViewResult
                {
                    ViewName = View,
                    MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }

            // log the error using log4net.

            //Creating Custom property to log the Controller Name and Action Name
            log4net.GlobalContext.Properties["ControllerActionLog"] = "Controller: " + controllerName + " Action: " + actionName;
            UserDetailModel objUserModel = (UserDetailModel)HttpContext.Current.Session[ApplicationConstants.Constants.LoggedInUser];
            if (objUserModel != null)
            {
            log4net.GlobalContext.Properties["UserName"] = objUserModel.UserName;
            }
            _logger.Error(filterContext.Exception.Message, filterContext.Exception);

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}