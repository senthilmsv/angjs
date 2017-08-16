using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiAsgRAS.Dashboard.Web.Filters
{
    public class GZipCacheFilterAttribute: ActionFilterAttribute 
    {
        /// <summary>
        /// Gets or sets the cache duration in seconds. Default 24 Hours.
        /// </summary>
        /// <value>The cache duration in seconds.</value>
        public int Duration {
            get;
            set;
        }

        public GZipCacheFilterAttribute()
        {
            Duration = 24;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Enable GZip Encoding
            var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(encodingsAccepted)) return;

            encodingsAccepted = encodingsAccepted.ToLowerInvariant();
            var response = filterContext.HttpContext.Response;

            if (response.ContentType == "text/html")
            {
                if (encodingsAccepted.Contains("gzip"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (encodingsAccepted.Contains("deflate"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
            //Enable Cacheability Public and set cache duration (24 hours)
            HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
            TimeSpan cacheDuration = TimeSpan.FromHours(Duration);

            cache.SetNoStore();
            cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            cache.SetExpires(DateTime.Now.Add(cacheDuration));
            cache.SetMaxAge(cacheDuration);
            cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
        }

        /// <summary>
        /// Determines if GZip is supported
        /// </summary>
        /// <returns></returns>
        public static bool IsGZipSupported()
        {
            string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
            if (!string.IsNullOrEmpty(AcceptEncoding) &&
                    (AcceptEncoding.Contains("gzip") || AcceptEncoding.Contains("deflate")))
                return true;
            return false;
        }


        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    HttpRequestBase request = filterContext.HttpContext.Request;
        //    HttpResponseBase response = filterContext.HttpContext.Response;
        //    string acceptEncoding = request.Headers["Accept-Encoding"];

        //    if (string.IsNullOrEmpty(acceptEncoding)) return;

        //    if (IsGZipSupported())
        //    {
        //        acceptEncoding = acceptEncoding.ToUpperInvariant();                

        //        if (acceptEncoding.Contains("GZIP"))
        //        {
        //            response.Headers.Remove("Content-Encoding");
        //            response.AppendHeader("Content-encoding", "gzip");
        //            response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);

        //        }
        //        else if (acceptEncoding.Contains("DEFLATE"))
        //        {
        //            response.Headers.Remove("Content-Encoding");
        //            response.AppendHeader("Content-encoding", "deflate");
        //            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
        //        }
        //    }
        //    // Allow proxy servers to cache encoded and unencoded versions separately
        //    response.AppendHeader("Vary", "Content-Encoding");

        //    ////Enable Cacheability Public and set cache duration (24 hours)
        //    //HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
        //    //TimeSpan cacheDuration = TimeSpan.FromHours(Duration);

        //    ////cache.SetNoStore();
        //    ////response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
        //    //cache.SetCacheability(HttpCacheability.ServerAndPrivate);
        //    //cache.SetExpires(DateTime.Now.Add(cacheDuration));
        //    //cache.SetMaxAge(cacheDuration);
        //    //cache.AppendCacheExtension("must-revalidate, proxy-revalidate");

        //}
    }
}