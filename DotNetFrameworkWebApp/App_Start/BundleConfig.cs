using System.Web;
using System.Web.Optimization;

namespace DotNetFrameworkWebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/globalJs").Include("~/Scripts/global.js"));
            #region DataTables
            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
               "~/Scripts/jquery.dataTables.js",
               "~/Scripts/dataTables.bootstrap4.js"
               ));
            #endregion
            #region [DATA TABLE]

            bundles.Add(new StyleBundle("~/bundles/DataTablesCss").Include(
             "~/Content/css/dataTables.bootstrap4.min.css"
             ));

            #endregion
        }
    }
}
