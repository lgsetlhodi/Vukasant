using System.Web;
using System.Web.Optimization;

namespace IQRecruitmentTool
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"

                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/contrets/sintjs").Include(
                 //LIBRARY INCLUDES
                 "~/Scripts/modernizr.custom.js",
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/cleave.js",
                      "~/Scripts/phonei8.js",
                      "~/Scripts/plugins.js",
                      "~/Scripts/scripts.js",
                           "~/Scripts/classie.js",
                       "~/Scripts/site.avatar.js",
                       "~/Scripts/jquery.form.js",
                       "~/Scripts/jquery.Jcrop.js",
                           "~/Scripts/dialogFx.js",
                        "~/Scripts/snap.svg-min.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/selectize.min.js",
                      "~/Scripts/toastr.min.js",

                      //APPLICATION INCLUDES
                      "~/Scripts/app/searchIdeas.js",
                      "~/Scripts/app/request.js",
                      "~/Scripts/app/actionRequest.js",
                      "~/Scripts/app/selectize-custom.js",
                      "~/Scripts/app/login.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/zintles").Include(
                      "~/Content/plugins.css",
                      "~/Content/theme.css",
                      "~/Content/et-line-icons.css",
                      "~/Content/flaticon.css",
                      "~/Content/icon-fonts.css",
                       "~/Content/ionicons.min.css",
                      "~/Content/themify-icons.css",
                      "~/Content/stylesheet.css",
                        "~/Content/custom.css",
                      "~/Content/colors/yellow.css",
                         "~/Content/normalize.css",
                           "~/Content/dialog.css",
                            "~/Content/stylesheet.css",
                       "~/Content/site.avatar.css",
                       "~/Content/jquery.Jcrop.css",
                           "~/Content/dialog-wilma.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/bootstrap-datepicker3.min.css",
                      "~/Content/bootstrap-datepicker3.standalone.css",
                      "~/Content/bootstrap-datepicker3.standalone.min.css",
                       "~/Content/selectize.bootstrap3.css",
                       "~/Content/toastr.min.css"
                      ));
        }
    }
}
