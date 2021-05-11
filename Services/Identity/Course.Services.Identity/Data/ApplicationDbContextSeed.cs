using Course.Services.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Identity.Data
{
    public static class ApplicationDbContextSeed
    {
        public async static Task Seed(IServiceProvider serviceProvider, IConfiguration configuration)
        {

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var email = configuration["Data:RootUser:email"];
            var password = configuration["Data:RootUser:password"];
            var userName = configuration["Data:RootUser:username"];

            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(new ApplicationUser { UserName = userName, Email = email }, password);
            }
 

        }
    }
}