using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GSearch.Api
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
            
            services.AddOptions().AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Console.WriteLine("123");
 
            //builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            logger.LogInformation("config...");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }




        //public Startup(IConfiguration configuration)
        //{
        //    //_logger = loggerFactory.CreateLogger<Startup>();

        //    Configuration = configuration;


        //}


        ////private readonly ILogger _logger;
        //public IConfiguration Configuration { get; }

        ////public ILifetimeScope AutofacContainer { get; private set; }

        //// ConfigureServices is where you register dependencies. This gets
        //// called by the runtime before the ConfigureContainer method, below.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add services to the collection. Don't build or return
        //    // any IServiceProvider or the ConfigureContainer method
        //    // won't get called.
        //    //services.AddOptions();
        //  services.AddControllers();
        //}

        //// ConfigureContainer is where you can register things directly
        //// with Autofac. This runs after ConfigureServices so the things
        //// here will override registrations made in ConfigureServices.
        //// Don't build the container; that gets done for you by the factory.
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    //_logger.LogInformation("Get to container");
        //    //builder.RegisterModule(new AutofacModule());
        //}

        //// Configure is where you add middleware. This is called after
        //// ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        //// here if you need to resolve things from the container.
        //public void Configure(
        //  IApplicationBuilder app,
        //  IWebHostEnvironment env)
        //{
        //    // If, for some reason, you need a reference to the built container, you
        //    // can use the convenience extension method GetAutofacRoot.
        //    //this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

        //    //_logger.LogInformation("Configure is called...");
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}

    }
}
