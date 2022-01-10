using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moldovan_Andrea_SADE_Proiect.Models;

namespace Moldovan_Andrea_SADE_Proiect.Data
{
    public class CafeContext : DbContext
    {
        public CafeContext(DbContextOptions<CafeContext> options) :
       base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SuppliedCoffee> SuppliedCoffees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<Coffee>().ToTable("Coffee");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
            modelBuilder.Entity<SuppliedCoffee>().ToTable("SuppliedCoffee");
            modelBuilder.Entity<SuppliedCoffee>()
            .HasKey(c => new { c.CoffeeID, c.SupplierID });//configureaza cheia primara compusa
        }
    }
}
