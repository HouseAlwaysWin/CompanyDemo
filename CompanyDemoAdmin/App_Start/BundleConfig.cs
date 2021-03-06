﻿using System.Web;
using System.Web.Optimization;

namespace CompanyDemoAdmin
{
    public class BundleConfig
    {
        // 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                       "~/Scripts/moment.min.js",
                       "~/Scripts/polyfll*",
                       "~/Scripts/axios*",
                       "~/Scripts/vue*",
                       "~/Scripts/veevalidate/locale/zh_TW.js",
                       "~/Scripts/veevalidate/vee-validate*",
                       "~/Scripts/element-ui/element-ui.js",
                       "~/Scripts/element-ui/locale/zh_TW.js"));

            bundles.Add(new ScriptBundle("~/bundles/veevalidate-customize-rule").Include(
                       "~/Scripts/veevalidate/customize-rule.js"));


            bundles.Add(new ScriptBundle("~/bundles/company").Include(
                            "~/Scripts/company/companyVue.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
              "~/Scripts/admin/adminVue.js"));

            bundles.Add(new ScriptBundle("~/bundles/showFrontUserLogin").Include(
              "~/Scripts/admin/showFrontUserLogin.js"));

            bundles.Add(new ScriptBundle("~/bundles/showBackUserLogin").Include(
            "~/Scripts/admin/showBackUserLogin.js"));



            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));


            bundles.Add(new ScriptBundle("~/bundles/sweetalert2").Include(
                       "~/Scripts/sweetalert2/sweetalert2*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/element").Include(
                      "~/Content/element-ui/element-ui-style.min.css"
                      ));


        }
    }
}
