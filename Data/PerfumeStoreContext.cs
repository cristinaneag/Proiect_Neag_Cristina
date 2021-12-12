using Microsoft.EntityFrameworkCore;
using Proiect_Neag_Cristina.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Data
{
    public class PerfumeStoreContext : DbContext
    {
        public PerfumeStoreContext(DbContextOptions<PerfumeStoreContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Perfume> Perfumes { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<ManufacturedPerfumes> ManufacturedPerfumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Perfume>().ToTable("Perfume");
            modelBuilder.Entity<Manufacturer>().ToTable("Brand");
            modelBuilder.Entity<Manufacturer>().ToTable("Manufacturer");
            modelBuilder.Entity<ManufacturedPerfumes>().ToTable("ManufacturedPerfumes");
            modelBuilder.Entity<ManufacturedPerfumes>()
            .HasKey(c => new { c.PerfumeID, c.ManufacturerID });
        }
    }
}

