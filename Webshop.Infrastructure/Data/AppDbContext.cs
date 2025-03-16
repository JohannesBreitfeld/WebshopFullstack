﻿using Webshop.Domain.Entities;
using Webshop.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistance.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        base.OnModelCreating(modelBuilder);
      
    }
}