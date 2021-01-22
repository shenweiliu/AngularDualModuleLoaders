using System.Web;
using System.Web.Optimization;

namespace SM.NgExDialog.Sample
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ngSystemJS").Include(
                //Use SystemJS as Angular module loader for local code work.                
                "~/ang-debug/shim.min.js",
                "~/ang-debug/npm-libs/zone.js/dist/zone.js",
                "~/ang-debug/npm-libs/reflect-metadata/Reflect.js",
                "~/ang-debug/npm-libs/systemjs/dist/system.src.js",
                "~/ang-debug/systemjs.config.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/ngCLI").Include(
                //Use Angular CLI as module loader and file bundles for all server environments.
                //Those js files are available after doing "ng build".
                //Build for prod merges vendor.js and main.js to one main.js file.
                "~/ang-dist/app/runtime.js",
                "~/ang-dist/app/polyfills-es5.js",
                "~/ang-dist/app/polyfills.js",
                "~/ang-dist/app/vendor.js",
                "~/ang-dist/app/main.js"
              ));

            bundles.Add(new StyleBundle("~/ngSystemJs/css").Include(
                "~/ang-debug/npm-libs/bootstrap/css/bootstrap.css",
                "~/ang-debug/npm-libs/font-awesome/css/font-awesome.css",
                "~/ang-content/src/assets/css/site.css",
                "~/ang-content/src/assets/css/ex-dialog.css"

               ));

            bundles.Add(new StyleBundle("~/ngCLI/css").Include(
                "~/ang-dist/app/styles.css"                
              ));
        }
    }
}
