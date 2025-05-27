using Microsoft.Extensions.DependencyInjection;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.MongoDb.UnitOfWork;

public class MongoUnitOfWork : IUnitOfWork
{
    public IProductRepository Products { get; }
    public IProductCategoryRepository ProductCategories { get; }
    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }

    public MongoUnitOfWork(
        IProductRepository products,
        IProductCategoryRepository categories,
        ICustomerRepository customers,
        IOrderRepository orders)
    {
        Products = products;
        ProductCategories = categories;
        Customers = customers;
        Orders = orders;
    }

    public void Dispose() {}
   
    public Task<int> SaveAsync() => Task.FromResult(0);
}
