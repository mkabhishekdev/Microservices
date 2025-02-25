using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using InventoryManagementAPI.Data;

namespace InventoryManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host =  BuildWebHost(args);
           using(var scope = host.Services.CreateScope())
           {
              var services = scope.ServiceProvider;
              try
              {
                 var context = services.GetRequiredService<InventoryContext>();
                 InventorySeed.SeedAsync(context).Wait();
              }
              catch(Exception ex)
              {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex,"Error has occured while data seeding");
              }
           }
           host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
