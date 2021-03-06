﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCoreWebAngularLearn.Data;
using DotNetCoreWebAngularLearn.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetCoreWebAngularLearn
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDataContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("MyDataConnectionString"));
            });

            services.AddAutoMapper();

            services.AddTransient<MySeeder>();

            services.AddScoped<IMyRepository, MyRepository>();

            services.AddTransient<IMailService, NullMailService>();

            //żeby nie było problemów musimy włączyć dependency injection bo tak działa ASP.NET core
            //używamy defaultowego servisu microsoftowego
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1) // dodalism tą linijkę przy tworzeniu API (CreateTheApi) 
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); // dodalismy przy tworzeniu API dla OrdersController


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
