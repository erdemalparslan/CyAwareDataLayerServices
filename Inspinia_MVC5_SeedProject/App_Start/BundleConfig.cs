﻿using System.Web;
using System.Web.Optimization;

namespace Inspinia_MVC5_SeedProject
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            // Vendor scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate.min.js"));

            // jQuery Validation - Unobtrusive
            bundles.Add(new ScriptBundle("~/bundles/jqueryValUnobt").Include(
            "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/app/inspinia.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimScroll/jquery.slimscroll.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/metisMenu").Include(
                      "~/Scripts/plugins/metisMenu/jquery.metisMenu.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/steps").Include(
                      "~/Scripts/plugins/steps/jquery.steps.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/mySteps").Include(
                      "~/Scripts/plugins/steps/mySteps.js"));

            bundles.Add(new ScriptBundle("~/plugins/jquery.dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/plugins/dataTables.bootstrap").Include(
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/plugins/dataTables.responsive").Include(
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js"));

            bundles.Add(new ScriptBundle("~/plugins/dataTables.tableTools.min").Include(
                      "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/style.css"));

            // CSS style (icheck)
            bundles.Add(new StyleBundle("~/Content/iCheck").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // CSS style (steps)
            bundles.Add(new StyleBundle("~/Content/steps").Include(
                      "~/Content/plugins/steps/jquery.steps.css"));

            // css style (dataTables.bootstrap)
            bundles.Add(new StyleBundle("~/Content/dataTables.bootstrap").Include(
                      "~/Content/plugins/dataTables/dataTables.bootstrap.css"));

            // css style (dataTables)
            bundles.Add(new StyleBundle("~/Content/dataTables.tableTools.min").Include(
                      "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // css style (dataTables)
            bundles.Add(new StyleBundle("~/Content/dataTables.responsive").Include(
                      "~/Content/plugins/dataTables/dataTables.responsive.css"));
        }
    }
}
