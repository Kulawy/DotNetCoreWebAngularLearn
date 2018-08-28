using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAngularLearn.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreWebAngularLearn
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMailService, NullMailService>();

            //żeby nie było problemów musimy włączyć dependency injection bo tak działa ASP.NET core
            //używamy defaultowego servisu microsoftowego
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //jeśli chcemy włączyć straszne strony przy wyrzucaniu błędów to app.UseDeveloperExceptionPage()
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            //jest spoko jak nie uzywamy ASP.NET core ale jak już zaczynamy uzywac to jest nam to nie potrzebne żeby się ładowały strony przez app.UseMvc
            //app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseNodeModules(env);

            app.UseMvc( cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", Action = "Index" });
            });


            //DEFAULT STUFF:
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
