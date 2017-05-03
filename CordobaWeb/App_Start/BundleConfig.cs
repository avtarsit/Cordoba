using System.Web;
using System.Web.Optimization;

namespace CordobaWeb
{
    public class BundleConfig
    {
        
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            ////////////////// Common /////////////////////////////

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/CommonJs").Include(
                    "~/Scripts/bootstrap.min.js",
                    "~/Scripts/bootstrap-modalmanager.js",
                    "~/Scripts/bootstrap-modal.js",
                    "~/Scripts/bootstrap-datepicker.js",
                    "~/Scripts/jquery.bootstrap-duallistbox.js",
                     "~/Scripts/bootbox.js",
                      "~/Scripts/bootstrap-modalmanager.js",
                       "~/Scripts/bootstrap-modal.js",
                       "~/Scripts/bootstrap-datepicker.js",
                       "~/Scripts/jquery.bootstrap-duallistbox.js",
                       "~/Scripts/bootbox.js"
                     ));
                   
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/textAngular.css",
                      "~/Content/css/bootstrap-duallistbox.css",
                      "~/Content/css/jquery.dataTables.min.css",
                      "~/Content/css/bootstrap-datepicker.min.css",
                      "~/Content/css/toastr.css",
                      "~/Content/css/site.css",
                      "~/Content/css/rzslider.css",
                      "~/Content/css/nav.css",                                              
                      "~/Content/css/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                     "~/Scripts/angular.js",
                     "~/Scripts/angular-animate.min.js",
                     "~/Scripts/angular-dragdrop.min.js",
                     "~/Scripts/angular-ui-router.min.js",
                     "~/Scripts/angular-local-storage.js",
                     "~/Scripts/angular-sanitize.js",
                     "~/Scripts/angular-datatables.min.js"
                     ));

   
            var appConfigBundle = new Bundle("~/bundles/appConfig");
            appConfigBundle.Include("~/Scripts/jquery.dataTables.min.js");
            appConfigBundle.Include("~/Scripts/dataTables.tableTools.js");
            appConfigBundle.Include("~/Scripts/jquery.dataTables.rowReordering.js");
            appConfigBundle.Include("~/Scripts/ng-file-upload.min.js");
            appConfigBundle.Include("~/Scripts/ng-file-upload-shim.min.js");
            appConfigBundle.Include("~/Scripts/textAngular-rangy.min.js");
            appConfigBundle.Include("~/Scripts/textAngular-sanitize.js");
            appConfigBundle.Include("~/Scripts/textAngular.min.js");
            appConfigBundle.Include("~/Scripts/jquery.responsiveTabs.js");
            appConfigBundle.Include("~/Scripts/menuscript.js");
            appConfigBundle.Include("~/Scripts/toastr.js");
            appConfigBundle.Include("~/Scripts/Chart.min.js");
            appConfigBundle.Include("~/JS/appConfiguration.js");
            appConfigBundle.Include("~/JS/Common.js");
            appConfigBundle.Include("~/Scripts/bsDuallistbox.js");
            appConfigBundle.IncludeDirectory("~/JS/Directives", "*.js", false);
            appConfigBundle.IncludeDirectory("~/JS/Factory", "*.js", false);
            appConfigBundle.IncludeDirectory("~/JS/Filters", "*.js", false);
            bundles.Add(appConfigBundle);



            //////////////////////////////////////////////////////////////////
            // this for Admin Theme
            bundles.Add(new StyleBundle("~/bundles/AdminCSS").Include(
                        "~/Content/admin/css/bootstrap.css",
                      "~/Content/admin/css/font-awesome.min.css",
                     "~/Content/admin/css/icons/icomoon/styles.css",                                    
                     "~/Content/admin/css/core.css",
                     "~/Content/admin/css/components.css",
                     "~/Content/admin/css/colors.css"));

            bundles.Add(new ScriptBundle("~/bundles/AdminJs").Include(
                      "~/Scripts/admin/js/plugins/loaders/pace.min.js",                      
                      "~/Scripts/admin/js/core/libraries/bootstrap.min.js",                      
                        "~/Scripts/admin/js/plugins/loaders/blockui.min.js",
                        "~/Scripts/admin/js/plugins/visualization/d3/d3.min.js",
                       "~/Scripts/admin/js/plugins/visualization/d3/d3_tooltip.js",
                       "~/Scripts/admin/js/plugins/forms/styling/switchery.min.js",
                       "~/Scripts/admin/js/plugins/forms/styling/uniform.min.js",
                       "~/Scripts/admin/js/plugins/forms/selects/bootstrap_multiselect.js",
                       "~/Scripts/admin/js/plugins/ui/moment/moment.min.js",
                       "~/Scripts/admin/js/plugins/pickers/daterangepicker.js",
                       "~/Scripts/admin/js/core/app.js",
                       "~/Scripts/admin/js/pages/dashboard.js"                       
                      ));

            var AdminControllerBundle = new Bundle("~/bundles/AdminControllers");
            AdminControllerBundle.IncludeDirectory("~/JS/Home", "*.js", false);
            AdminControllerBundle.IncludeDirectory("~/JS/Test", "*.js", false);
            AdminControllerBundle.IncludeDirectory("~/JS/Admin", "*.js", true);
            bundles.Add(AdminControllerBundle);  
      
           /////////////////////////
        
            var controllerBundle = new Bundle("~/bundles/Controllers");
            controllerBundle.IncludeDirectory("~/JS/Home", "*.js", false);
            controllerBundle.IncludeDirectory("~/JS/Test", "*.js", false);
            bundles.Add(controllerBundle);

            

        }
    }
}
