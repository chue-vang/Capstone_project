using HersFlowers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using HersFlowers.EmailService;

namespace HersFlowers.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }
        public DbSet<MailRequest> MailRequests { get; set; }
        
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

            builder.Entity<Flower>()
            .HasData(
             new Flower
             {
                 Id = 1,
                 Name = "Bouquet",
                 Price = 15,
             }
             );
        }
        public DbSet<HersFlowers.EmailService.MailRequest> MailRequest { get; set; }
    }
}
