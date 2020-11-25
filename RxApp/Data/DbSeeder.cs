using Microsoft.AspNetCore.Identity;
using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data
{
    public class DbSeeder
    {


        public static async void SeedDb(RxAppContext context, RoleManager<IdentityRole> RoleManager, UserManager<Customer> userManager) 
        {
            context.Database.EnsureCreated();

            IdentityRole admin = new IdentityRole
            {
                Name = "admin"
            };
            IdentityRole user = new IdentityRole
            {
                Name = "user"
            };
            RoleManager.CreateAsync(admin).Wait();
            RoleManager.CreateAsync(user).Wait();
            var Admin = new Customer { UserName = "Admin", Email = "yaroslav@gmail.com", WorkName = "Nure"};
            userManager.CreateAsync(Admin, "P@sswordAdmin#").Wait();
            context.SaveChanges();
            var getAdmin = await userManager.FindByEmailAsync("selezznyovkirill@gmail.com");
            //userManager.AddToRoleAsync(getAdmin, "Admin").Wait();           
        }
    }
}
