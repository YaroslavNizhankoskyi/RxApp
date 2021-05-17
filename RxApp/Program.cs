using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RxApp.Data;

namespace RxApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<RxAppContext>();

                await context.Database.MigrateAsync();

                if (!await roleManager.RoleExistsAsync("Admin"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                if (!await roleManager.RoleExistsAsync("Patient"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Patient" });
                if (!await roleManager.RoleExistsAsync("Pharmacist"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Pharmacist" });
                if (!await roleManager.RoleExistsAsync("Medic"))
                    await roleManager.CreateAsync(new IdentityRole { Name = "Medic" });

            }
            catch (Exception ex) 
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Ann error occured during igrations");
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
