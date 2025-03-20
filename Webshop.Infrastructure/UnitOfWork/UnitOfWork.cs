using Fullstack.Persistance.Data;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; }
    public IProductCategoryRepository ProductCategories { get; }
    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }

    public UnitOfWork(
        AppDbContext context, 
        IProductRepository productRepository, 
        IProductCategoryRepository productCategoryRepository, 
        ICustomerRepository customerRepository, 
        IOrderRepository orderRepository)
    {
        _context = context;
        Products = productRepository;
        ProductCategories = productCategoryRepository;
        Customers = customerRepository;
        Orders = orderRepository;
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
