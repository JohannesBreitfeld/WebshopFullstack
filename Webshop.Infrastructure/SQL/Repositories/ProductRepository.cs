using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.SQL.Data;

namespace Webshop.Infrastructure.SQL.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Where(p => p.SoftDeleted == false).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    
    public async Task<Product?> GetByNameAsync(string name) => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

    public async Task AddAsync(Product product) => await _context.Products.AddAsync(product);

    public void Update(Product product) => _context.Products.Update(product);
    
    public void Delete(Product product) => _context.Products.Remove(product);

    public Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}
