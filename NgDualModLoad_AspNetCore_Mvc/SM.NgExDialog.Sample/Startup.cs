using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;

namespace SM.NgDataCrud.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();        
                    });
            });

            //services.AddControllers();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseExceptionHandler(errorApp =>
                //{
                //    errorApp.Run(async context =>
                //    {
                //        context.Response.StatusCode = 500;
                //        context.Response.ContentType = "text/plain";
                //        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                //        if (errorFeature != null)
                //        {
                //            var logger = loggerFactory.CreateLogger("Global exception logger");
                //            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                //        }

                //        await context.Response.WriteAsync("There was an error");
                //    });
                //});
            }

            app.Use(async (context, next) =>
            {
                await next(); // then hit by the INCOMING request do nothing but pass the request to the next middleware (DefaultFiles)

                // Then hit by the OUTGOING request test if the application have matched any resource
                // - if a resource was found (200 Ok) then do nothing but let the request travel further out
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    // - if on the other hand a resource was not found (404 Not found) then assume it is an Angular url so ..
                    // .. reset the path and call await next(); to make the request INCOMING again ..
                    // .. the request will now be passed to DefaultFiles and then to StaticFiles serving index.html
                    // .. after StaticFiles the request will turn to OUTGOING eventually coming back here this time with 200 Ok. 

                    var refreshBaseUrl = "/";
                    context.Request.Path = new PathString(refreshBaseUrl);
                    await next();
                }
            });

            app.UseStaticFiles(); // For the wwwroot folder.

            ////Set ang-content folder as static accessble.
            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(env.ContentRootPath, "ang-content")),
            //    RequestPath = "/ang-content"
            //    //EnableDirectoryBrowsing = true
            //});

            app.UseCookiePolicy();
            app.UseCors("AllowAllOrigins");
            
            app.UseRouting();
            
            //app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                                
                //endpoints.MapFallbackToFile("/"); //Not working for MVC.
            });
        }
    }
}
