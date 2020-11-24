using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RxApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RxApp.Data
{
    public class RxAppContext : IdentityDbContext<Customer>
    {


        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }

        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<DrugActiveIngredient> DrugActiveIngredients { get; set; }

        public DbSet<PharmGroup> PharmGroups { get; set; }

        public DbSet<Recipe> Recipes{ get; set; }

        public DbSet<RecipeDrug> RecipeDrugs { get; set; }

        public DbSet<IncompatibleIngredient> IncompatibleIngredients { get; set; }



        public RxAppContext(DbContextOptions<RxAppContext> options)
        : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Drug>()
                .HasMany(c => c.DrugActiveIngredients)
                .WithOne(c => c.Drug)
                .HasForeignKey(c => c.DrugId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Drug>()
                .HasOne(c => c.PharmGroup)
                .WithMany(c => c.Drugs)
                .HasForeignKey(c => c.PharmGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Allergy>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Allergies)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Allergy>()
                .HasOne(c => c.ActiveIngredient)
                .WithMany(c => c.Allergies)
                .HasForeignKey(c => c.ActiveIngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Recipe>()
                .HasMany(c => c.RecipeDrugs)
                .WithOne(c => c.Recipe)
                .HasForeignKey(c => c.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(builder);

        }
    }



}
