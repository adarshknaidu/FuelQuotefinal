using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FuelQuoteApp.EntityModels;


namespace FuelQuoteApp.DataLayer.Models
{
    public class FuelQuoteDBContext : IdentityDbContext
    {
        public FuelQuoteDBContext(DbContextOptions<FuelQuoteDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Client> Client { get; set; }

        public DbSet<User> UsersInfo { get; set; }

        public DbSet<Quote> FuelQuote { get; set; }

    }
}


