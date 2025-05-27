using Microsoft.EntityFrameworkCore;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.SQL.Data;

namespace Webshop.Infrastructure.SQL.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AppDbContext _context;

    public ProductCategoryRepository(AppDbContext context) => _context = context;
    
    public async Task<IEnumerable<ProductCategory>> GetAllAsync() => await _context.ProductCategories.ToListAsync();
    
    public async Task<ProductCategory?> GetByIdAsync(int id) => await _context.ProductCategories.FindAsync(id);
    
    public async Task AddAsync(ProductCategory category) => await _context.ProductCategories.AddAsync(category);
    
    public void Update(ProductCategory category) => _context.ProductCategories.Update(category);
    
    public void Delete(ProductCategory category) => _context.ProductCategories.Remove(category);
}
