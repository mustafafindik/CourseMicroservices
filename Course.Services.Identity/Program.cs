using Course.Services.Identity.Data;
using Course.Services.Identity.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var serviceProvider = scope.ServiceProvider;
            //    var config = serviceProvider.GetRequiredService<IConfiguration>();

            //    await ApplicationDbContextSeed.CreateRootAdmin(serviceProvider, config);
            //}

            //using (var scope = host.Services.CreateScope())
            //{
            //    var serviceProvider = scope.ServiceProvider;



            //    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //    if (!userManager.Users.Any())
            //    {
            //        userManager.CreateAsync(new ApplicationUser { Email = "f-cakiroglu@outlook.com" }, "1907abcdXX@").Wait();
            //    }
            //}

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
