using HersFlowers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HersFlowers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<ShoppingCarItem> ShoppingCarItems { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Image> Images { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Owner",
                NormalizedName = "OWNER"
            }
            ,

            new IdentityRole
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
            );

            builder.Entity<Owner>()
            .HasData(
             new Owner
             {
                 Id = 1,
                 FirstName = "Aerith",
                 LastName = "Gainsborough",
                 Email = "aerith@owner"
             });

            builder.Entity<Customer>()
            .HasData(
             new Customer
             {
                 Id = 1,
                 FirstName = "Cloud",
                 LastName = "Strife",
                 Email = "cloud@customer",
                 PhoneNumber = 7777777777,
             });

            builder.Entity<Flower>()
            .HasData(
             new Flower
             {
                 Id = 1,
                 Name = "large boquet",
                 Price = 15,
             });

            builder.Entity<Flower>()
            .HasData(
            new Flower
            {
                Id = 2,
                Name = "small boquet",
                Price = 10,
            });
        }
    }
}
