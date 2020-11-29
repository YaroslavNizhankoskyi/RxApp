using Microsoft.AspNetCore.Identity;
using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data.Seed
{
    public class DbSeeder
    {


        public static void SeedDb(RxAppContext context) 
        {
            context.Database.EnsureCreated();


                       
        }
    }
}
