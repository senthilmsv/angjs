using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HiAsgRAS.Dashboard.Web.Common
{
    public static class HtmlMenu
    {
        public static MvcHtmlString MenuLink(this HtmlHelper helper,
                                    string text, string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            if (String.Equals(action, currentAction as string, StringComparison.OrdinalIgnoreCase)
            &&
            String.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase))
            {
                return new MvcHtmlString("<li class=\"active\">" + helper.ActionLink(text, action, controller) + "</li>");
            }
            //else if (String.Equals(controller.ToLower(), "home") &&
            //         String.Equals(action.ToLower(), "index"))
            //{
            //    Random rnd = new Random();
            //    RouteValueDictionary routeDic = new RouteValueDictionary();
            //    routeDic.Add("rnd", rnd.Next().ToString());

            //    //var redirectUrl = new UrlHelper(helper.ViewContext.HttpContext.Request.RequestContext).Action("index", "home", routeDic);
            //    var url = helper.ActionLink("Dashboard", "index", "home");
            //    return new MvcHtmlString("<li >" + url + "</li>");
            //}            
            //string scheme = helper.ViewContext.HttpContext.Request.Url.Scheme;
            return new MvcHtmlString("<li>" + helper.ActionLink(text, action, controller) + "</li>");
        }


        public static string MenuActive(this HtmlHelper helper,
                                        string action, string controller)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            var currentController = routeData["controller"];
            var currentAction = routeData["action"];

            if (String.Equals(action, currentAction as string, StringComparison.OrdinalIgnoreCase)
                && String.Equals(controller, currentController as string, StringComparison.OrdinalIgnoreCase))
            {
                return "active";
            }
            return "normalli";
        }

    }
}