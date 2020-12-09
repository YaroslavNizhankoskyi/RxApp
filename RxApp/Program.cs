using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RxApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                if (!await roleManager.RoleExistsAsync("Patient"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Patient" });
                if (!await roleManager.RoleExistsAsync("Pharmacist"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Pharmacist" });
                if (!await roleManager.RoleExistsAsync("Medic"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Medic" });
                

            }

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
