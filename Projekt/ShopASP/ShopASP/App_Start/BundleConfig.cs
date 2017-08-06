using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ShopASP.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/sheets").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css"
                ));
            bundles.Add(new StyleBundle("~/bundles/jquery-ui-sheets").Include(
                "~/Content/themes/base/autocomplete.css",
                "~/Content/themes/base/core.css",
                "~/Content/themes/base/theme.css",
                "~/Content/themes/base/menu.css"
                ));
        }
    }
}