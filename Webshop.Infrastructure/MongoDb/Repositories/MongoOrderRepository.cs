using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoOrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection;

    public MongoOrderRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Order>("Orders");
    }

    public async Task AddAsync(Order order) => await _collection.InsertOneAsync(order);

    public async Task<IEnumerable<Order>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId) => await _collection.Find(o => o.CustomerId == customerId).ToListAsync();

    public async Task<Order?> GetByOrderIdAsync(int orderId) => await _collection.Find(o => o.Id == orderId).FirstOrDefaultAsync();
}
