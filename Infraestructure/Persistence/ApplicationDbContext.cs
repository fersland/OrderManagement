using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.IdentityModel.Abstractions;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Films> Films => Set<Films>();
        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<Developer> Developers => Set<Developer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);
                builder.Property(o => o.CustomerName).HasMaxLength(100).IsRequired();
                builder.Property(o => o.CustomerEmail).HasMaxLength(150).IsRequired();
                builder.Property(o => o.TotalAmount).HasPrecision(18, 2);

                builder.HasMany(o => o.items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(builder =>
            {
                builder.HasKey(i => i.Id);
                builder.Property(i => i.ProductName).HasMaxLength(150).IsRequired();
                builder.Property(i => i.UnitPrice).HasPrecision(18,2);
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.FirstName).HasMaxLength(40).IsRequired();
                builder.Property(c => c.LastName).HasMaxLength(40).IsRequired();
                builder.Property(c => c.Ci).HasMaxLength(13).IsRequired();
            });

            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Description).HasMaxLength(100).IsRequired();
                builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
                builder.Property(p => p.Stock);
            });

            modelBuilder.Entity<Department>(builder =>
            {
                builder.HasKey(d => d.Id);
                builder.Property(d => d.Name).HasMaxLength(200).IsRequired();
                builder.Property(d => d.Code).HasMaxLength(20);
            });

            modelBuilder.Entity<Films>(builder =>
            {
                builder.HasKey(f => f.Id);
                builder.Property(f => f.Title).HasMaxLength(200).IsRequired();
                builder.Property(f => f.Description);
                builder.Property(f => f.ReleaseYear).IsRequired();
            });

            modelBuilder.Entity<Pet>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
                builder.Property(p => p.Colour).HasMaxLength(50);
                builder.Property(p => p.Age).IsRequired();
            });

            modelBuilder.Entity<Developer>(builder =>
            {
                builder.HasKey(d => d.Id);
                builder.Property(d => d.Name).HasMaxLength(60).IsRequired();
                builder.Property(d => d.Age).IsRequired();
            });
        }
    }
}
