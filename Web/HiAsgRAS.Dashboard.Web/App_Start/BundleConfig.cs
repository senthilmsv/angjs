using System.Web;
using System.Web.Optimization;

namespace HiAsgRAS.Dashboard.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.unobtrusive*",
            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            /*
             * Site Related js files for all pages will be in one bundle* 
             */
            bundles.Add(new ScriptBundle("~/bundles/SiteReqJs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/moment.js",
                        "~/assets/js/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/assets/js/material.js",
                        "~/assets/js/bootstrap-notify.js",
                        "~/assets/js/material-dashboard.js",
                        "~/assets/js/daterangepicker.js",
                        "~/assets/js/sweetalert.js"
                        ));

            //<!-- DataTables Plugin-->
            bundles.Add(new ScriptBundle("~/bundles/dataTableJs").Include(
                        "~/assets/js/jquery.dataTables.js",
                        "~/assets/js/dataTables.bootstrap.js",
                        "~/assets/js/dataTables.checkboxes.js"));

            //<!-- DataTables Plugin Support Plugins-->
            //bundles.Add(new ScriptBundle("~/bundles/dataTableExportPlugins")
            //            .IncludeDirectory("~/assets/js/Datatable/", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTableExportPlugins").Include(
                       "~/assets/js/DataTable/dataTables.buttons.js",
                       "~/assets/js/DataTable/buttons.flash.js",
                       "~/assets/js/DataTable/jszip.js",
                       "~/assets/js/DataTable/pdfmake.js",
                       "~/assets/js/DataTable/vfs_fonts.js",
                       "~/assets/js/DataTable/buttons.html5.js",
                       "~/assets/js/DataTable/buttons.print.js"
                       ));

            //<!--  Charts Plugin -->
            bundles.Add(new ScriptBundle("~/bundles/chartist").Include(
                        "~/assets/js/chartist.js",
                        "~/assets/js/chartist-plugin-tooltip.js",
                        "~/assets/js/chartist-plugin-pointlabels.js"));

            //Combine Dashboard page related Js into one file
            bundles.Add(new ScriptBundle("~/bundles/dashboardPageJs").Include(
                        "~/assets/js/jquery.dataTables.js",
                        "~/assets/js/dataTables.bootstrap.js",
                        "~/assets/js/chartist.js",
                        "~/assets/js/chartist-plugin-tooltip.js",
                        "~/assets/js/chartist-plugin-pointlabels.js",
                         "~/assets/js/dataTables.checkboxes.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/appsJs")
                        .IncludeDirectory("~/scripts/apps/", "*.js"));


            /*************************************************************************************************/
            //css

            bundles.Add(new StyleBundle("~/Content/SiteReqCss").Include(
                        "~/assets/css/bootstrap.css"
                        ,"~/assets/css/material-dashboard.css"
                        ,"~/assets/css/dashboard.css"
                        ,"~/assets/css/font-awesome.css"
                        ,"~/assets/css/font-material-icons.css"
                        ,"~/assets/css/loader.css"
                        ,"~/assets/css/daterangepicker.css"
                        ,"~/assets/css/animate.css"
                        ,"~/assets/css/sweetalert.css"                        
                        ));

            bundles.Add(new StyleBundle("~/Content/dataTablecss").Include(
                        "~/assets/css/dataTables.bootstrap.css"
                        , "~/assets/css/dataTables.checkboxes.css"
                        , "~/assets/css/buttons.dataTables.css"));

            bundles.Add(new StyleBundle("~/Content/chartcss").Include(
                        "~/assets/css/chartist-plugin-tooltip.css"));

            //BundleTable.EnableOptimizations = true;
        }
    }
}
