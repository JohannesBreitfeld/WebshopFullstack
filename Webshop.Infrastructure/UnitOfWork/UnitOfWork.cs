﻿using Fullstack.Persistance.Data;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository Products { get; }

    public UnitOfWork(AppDbContext context, IProductRepository productRepository)
    {
        _context = context;
        Products = productRepository;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
