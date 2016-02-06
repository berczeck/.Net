using System;
using System.Web.Optimization;

namespace BundlingMinification
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/style/vendor")
                    .Include(
                        "~/Css/normalize.css"
                    )
                );

            bundles.Add(
                new StyleBundle("~/style/app")
                    .Include(
                        "~/Css/StyleBody.css",
                        "~/Css/StyleButton.css",
                        "~/Css/StyleLabel.css"
                    )
                );

            bundles.Add(
                new StyleBundle("~/js/vendor")
                    .Include(
                        "~/Scripts/jquey-2.1.0.js"
                    )
                );

            //if (!#DEBUG)
            {
                BundleTable.EnableOptimizations = true;
            }
        }
    }
}