using Fullstack.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    
    public async Task<Product?> GetByNameAsync(string name) => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

    public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await Task.CompletedTask;
    }
}
