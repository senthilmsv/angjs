using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HiAsgRAS.Dashboard.Web.Common
{
    public class CommonWeb:Page
    {

        public static class CommonUtilities
        {

            public static IEnumerable<SelectListItem> GetSelectListItem(IEnumerable<dynamic> valueList, string dataValueCol, string dataTextCol)
            {
                if (valueList != null)
                {
                    return new SelectList(valueList, dataValueCol, dataTextCol);
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }

            public static IEnumerable<SelectListItem> GetSelectListItem(IEnumerable<dynamic> valueList, 
                string dataValueCol, string dataTextCol, object defaultValue)
            {
                if (valueList != null)
                {
                    return new SelectList(valueList, dataValueCol, dataTextCol, defaultValue);
                }
                else
                {
                    return new List<SelectListItem>();
                }
            }

            public static string TimeAgo(DateTime dt)
            {
                TimeSpan span = DateTime.Now - dt;
                if (span.Days > 365)
                {
                    int years = (span.Days / 365);
                    if (span.Days % 365 != 0)
                        years += 1;
                    return String.Format("Monitored {0} {1} ago",
                    years, years == 1 ? "year" : "years");
                }
                if (span.Days > 30)
                {
                    int months = (span.Days / 30);
                    if (span.Days % 31 != 0)
                        months += 1;
                    return String.Format("Monitored {0} {1} ago",
                    months, months == 1 ? "month" : "months");
                }
                if (span.Days > 0)
                    return String.Format("Monitored {0} {1} ago",
                    span.Days, span.Days == 1 ? "day" : "days");
                if (span.Hours > 0)
                    return String.Format("Monitored {0} {1} ago",
                    span.Hours, span.Hours == 1 ? "hour" : "hours");
                if (span.Minutes > 0)
                    return String.Format("Monitored {0} {1} ago",
                    span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
                if (span.Seconds > 5)
                    return String.Format("Monitored {0} seconds ago", span.Seconds);
                if (span.Seconds <= 5)
                    return "just now";
                return string.Empty;
            }
        }

    }

    public class CustomCommonMethods
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
               

        public static void LogException(string ControllerActionLog, string UserName, Exception ex)
        {
            //Creating Custom property to log the Controller Name and Action Name
            log4net.GlobalContext.Properties["ControllerActionLog"] = ControllerActionLog;

            log4net.GlobalContext.Properties["UserName"] = UserName;
            _logger.Error(ex.Message.ToString(), ex);
        }
    }
}


