## Angular Dual Module Loaders for Visual Studio Projects

Optimizing Angular debug and release environments with auto switchable SystemJS and Angular CLI module loaders in Visual Studio projects.

### Why Dual Module Loaders?

The SystemJS module loader has been deprecated and replaced by the Webpack or Angular CLI (internally using the Webpack) for the application development with the Angular. But code work and debugging advantages of the SystemJS, especially in Visual Studio projects, could never all be replaced by the Webpack even with its hot module replacement (HMR) feature. For developers who work on Angular applications but love the same experience of Visual Studio built-in debugging with the vanilla JavaScript, jQuery, Knockout.js, or even AngularJS, this auto dual module loader approach can be tried to increase the code work productivity and optimize both development and production operatiolns of the applications.

### Prerequisites on Local Machine

1. Visual Studio 2019 version 16.8.x for ASP.NET Core related projects. Visual studio 2017 or above for ASP.NET MVC 5 related projects.

2. [TypeScript 4.0 for Visual Studio](https://marketplace.visualstudio.com/items?itemName=TypeScriptTeam.typescript-40) (it is automatically installed with the Visual Studio 2019 version 16.8.x)

3. [node.js](https://nodejs.org/en/) (recommended version 14.x or above).

4. [Angular CLI](https://www.npmjs.com/package/@angular/cli) (recommended version 11.x or above).

5. Gulp installed globally (running: `npm install -g gulp`).

### Setting Up Projects

Four ASP.NET project templates with the Angular 11 are included in the code repository. Both *NgDualModLoad_AspNetCore_Spa* and *NgDualModLoad_AspNet_Spa* applications are initiated from a regular HTML page, wheras both *NgDualModLoad_AspNetCore_Mvc* and *NgDualModLoad_AspNet_Mvc* applications are started with the MVC default route and CSHtml Razor page.

For any template of the projects, do the following steps: 

1. Go to the directory *ang-content* or *wwwroot/ang-content* of the project directory with the Command Prompt, install the `node_modules` packages:

    `npm install`

2. In the same *ang-content* directory, Build the Angular output files for non-development environments:

    `ng build --prod`

3. Copy the small set of package files needed for the references in the development environment. Direct references to the files in the `node-modules` could work but many unnecessary files are also loaded resulting a bad performance. Do the command in the *ang-content* or *wwwroot/ang-content* directory to create the *npm-libs* folder under the *ang-debug*:

    `gulp copyLibs`

NOTE: like the *node_modules*, the *npm-libs* folder is reproducible from the development environment so that it may not be checked-in to the repository.

4. Open the solution you would like with the Visual Studio and rebuild it.

### Running Applications

Any of the Visual Studio solution contains a demo for the [NgExDialog](https://www.codeproject.com/Articles/1179258/An-Angular-Modal-Dialog-with-Advanced-Functionalit) sample application which is simply used for illustrating the Angular workflow and debugging practices. In the Visual Studio, select the supported browser, Chrome, IE11, or Edge, from the **IIS Express** dropdown on the toolbar, and then start the application by clicking the **IIS Express** or press **F5**.

You can set any breakpoint on the valid code line in any ts file with any built-in functionality and window for debugging processes as the same debugging operations as for the server-side C# or the vanilla JavaScript code. You can also notice that the original, not the dynamic, *ts* file and code are available for your debugging work.

### Auto Switching to Angular CLI for Release

The dual module loader configuration uses the SystemJS for the development/debug environment and pure Angular CLI for the non-development environment with the same code base in the *ang-content* directory. No code or manual intervention, regarding the client processes with the Angular code, is needed when the application is released to any non-development environment. If using the DevOps pipeline for the deployment, you may directly copy the Angular CLI output files from the *ang-dist/app* folder to the server box if setting the *ang-dist/app* directory as the repository checked-in content, or run the `ng build --prod` for the code base of the *ang-content* directory on the DevOps.

You can also change the environment-related settings in the Visual Studio to test the outcomes for non-development environments from your local machine.

NOTE: make sure to clear the browser cache after any change for the settings and before re-running the application.

***NgDualModLoad_AspNetCore_Spa*** or ***NgDualModLoad_AspNetCore_Mvc***   

Change the **Project Properties** > **Debug** > **Environment Variables** > **ASPNETCORE_ENVIRONMENT** from `Development` to `Production`.

***NgDualModLoad_AspNet_Spa***

Uncomment the below lines in the web.config file for re-mapping the URL to default Angular CLI starting HTML page:

    <!--<urlMappings enabled="true" >
      <add url="~/" mappedUrl="~/ang-dist/app/index-cli.html"/>
    </urlMappings>-->        

NOTE: In the normal release scenario, this `urlMappings` node takes in effect in the *Web.Release.config* (or other equivalents, such as *Web.Prod.config*) file through the `xdt.Transform`. Please see the code details in the *Web.Release.config* file. 

***NgDualModLoad_AspNet_Mvc***

Comment out below line in the *Web.config* file or set the value to “false”:

    <add key="IsAngularDebugLoader" value="true"/>

NOTE: In the normal release scenario, this key is removed from the *Web.Release.config* (or other equivalents, such as *Web.Prod.config*) file through the `xdt.Transform`. Please see the code details in the *Web.Release.config* file. 
