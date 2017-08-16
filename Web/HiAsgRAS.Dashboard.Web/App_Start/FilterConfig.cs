using HiAsgRAS.Dashboard.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            //filters.Add(new GZipCacheFilterAttribute());
            filters.Add(new NoCacheAttribute());
        }
    }
}
