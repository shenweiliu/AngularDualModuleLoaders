(function (global) {
    SystemJS.typescriptOptions = {
        "target": "es5",
        "module": "system",
        "moduleResolution": "node",
        "sourceMap": true,
        "emitDecoratorMetadata": true,
        "experimentalDecorators": true,
        "noImplicitAny": true,
        "suppressImplicitAnyIndexErrors": true
    };
    System.config({
        transpiler: 'ts',
        meta: {
            'typescript': {
                "exports": "ts"
            }
        },
        paths: {
            //paths serve as alias

            //'npm:': 'https://unpkg.com/' //external path to libraries for CDN (Content Delivery Network) delivery.
            'npm:': '/ang-debug/npm-libs/' //custom local path to libraries.  
            //'npm:': '/node_modules/' //direct node_module path - will load all releted files and very slow.
        },

        // map tells the System loader where to look for things
        map: {
            //System.import('identifier') will try to match any property name in the map object,
            //so System.import('app') will map the app property.
            //The map property must specify the location of our Angular application,
            //so app: 'app' means that our application is located in wwwroot\app.
            //Say that our application was located in wwwroot\dist,
            //then we would need to have app: 'dist',
            //and still System.import('app').
            app: '/ang-content/src',

            //angular bundles 
            '@angular/animations': 'npm:@angular/animations/bundles/animations.umd.js',
            '@angular/animations/browser': 'npm:@angular/animations/bundles/animations-browser.umd.js',
            '@angular/platform-browser/animations': 'npm:@angular/platform-browser/bundles/platform-browser-animations.umd.js', 
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js', 
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',             
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',            
            '@angular/common/http': 'npm:@angular/common/bundles/common-http.umd.js',            
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
			'@angular/upgrade': 'npm:@angular/upgrade/bundles/upgrade.umd.js',
            '@angular/upgrade/static': 'npm:@angular/upgrade/bundles/upgrade-static.umd.js',
            //other libraries
            'tslib': 'npm:tslib/tslib.js',
            'rxjs': 'npm:rxjs'  
        },

        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            //This app must match the map.app so that we can specify the main entry point: ./main.js
            //, so if we had map.myApp then we would need to have packages.myApp.
            app: {
                main: './main.js',
                //any module argument in code belonging to the wwwroot\app folder must be postfixed .js to match the filename.
                defaultExtension: 'js'
            },
            rxjs: { main: 'index.js', defaultExtension: 'js' },
            'rxjs/operators': { main: 'index.js', defaultExtension: 'js' }
        }
    });
})(this);

System.import('app').catch(function (err) { console.error(err); });