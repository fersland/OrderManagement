using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infraestructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Customer> Customers => Set<Customer>();

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
        }
    }
}
