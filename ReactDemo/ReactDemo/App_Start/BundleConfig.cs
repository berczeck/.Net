using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Optimization.React;

namespace ReactDemo
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new BabelBundle("~/bundles/main").
                Include( "~/Scripts/Tutorial.jsx", "~/Scripts/showdown.js")
                );

            //BundleTable.EnableOptimizations = true;
        }
    }
}