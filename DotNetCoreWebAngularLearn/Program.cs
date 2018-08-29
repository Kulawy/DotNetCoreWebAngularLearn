using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebAngularLearn.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; // żeby móc użyć GetService<MySeeder>
using Microsoft.Extensions.Logging;

namespace DotNetCoreWebAngularLearn
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = CreateWebHostBuilder(args).Build();

            SeedDB(host);

            host.Run();

            //CreateWebHostBuilder(args).Build().Run();
        }

        private static void SeedDB(IWebHost host)
        {

            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();


            using ( var scope = scopeFactory.CreateScope() )
            {
                //var seeder = host.Services.GetService<MySeeder>(); jeśli mamy scope to teraz:
                var seeder = scope.ServiceProvider.GetService<MySeeder>();
                seeder.Seed(); 
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //removing the default configuration options
            builder.Sources.Clear();

            builder.AddJsonFile("configDb.json", false, true)
//                .AddXmlFile("configDb.xml", true)
                .AddEnvironmentVariables();

        }
    }
}

//CreateDefaultBuilder defaultowo tworzy już configuration dla bazy danych ale my to zrobimy manualnie żeby wiedzieć co sie odwala
//public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//            WebHost.CreateDefaultBuilder(args)
//                .UseStartup<Startup>();
