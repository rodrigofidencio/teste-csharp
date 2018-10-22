using System.Web;
using System.Web.Optimization;

namespace fundagMVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                    "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                      "~/Scripts/metisMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryslimscroll").Include(
                   "~/Scripts/jquery.slimscroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerysparkline").Include(
                   "~/Scripts/jquery.sparkline.js"));

            bundles.Add(new ScriptBundle("~/bundles/multiselect").Include(
                   "~/Scripts/bootstrap-multiselect.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                   "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datepicker").Include(
                    "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
            "~/Scripts/select2.js"));



            bundles.Add(new ScriptBundle("~/bundles/jquerymaskedinput").Include(
                   "~/Scripts/jquery.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                  "~/Scripts/common.js"));

            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                      "~/Scripts/core.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-multiselect.css",
                      "~/Content/toastr.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/metisMenu.css",
                      "~/content/jquery-ui.css",
                      "~/content/select2.css",
                      "~/Content/style.css"));
        }
    }
}
