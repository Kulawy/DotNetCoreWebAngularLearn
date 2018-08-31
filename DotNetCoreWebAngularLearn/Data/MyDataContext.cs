using DotNetCoreWebAngularLearn.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreWebAngularLearn.Data
{
    public class MyDataContext : DbContext
    {

        public MyDataContext(DbContextOptions<MyDataContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2");



        }

    }
}
