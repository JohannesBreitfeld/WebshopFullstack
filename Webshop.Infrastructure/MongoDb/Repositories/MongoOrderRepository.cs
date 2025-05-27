using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;
using Webshop.Infrastructure.MongoDb.SequenceServices;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoOrderRepository : IOrderRepository
{
    private readonly IMongoCollection<MongoOrder> _collection;
    private readonly SequenceService _sequenceService;

    public MongoOrderRepository(IMongoDatabase database, SequenceService sequenceService)
    {
        _collection = database.GetCollection<MongoOrder>("Orders");
        _sequenceService = sequenceService;
    }

    public async Task AddAsync(Order order)
    {
        var nextId = await _sequenceService.GetNextSequenceValueAsync("Orders");
        order.Id = nextId;
        var mongoOrder = order.MapToMongo();
        await _collection.InsertOneAsync(mongoOrder);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var mongoOrders = await _collection
            .Find(_ => true)
            .ToListAsync();
        var orders = mongoOrders?
            .Select(mo => mo.MapToDomain())
            .ToList() 
            ?? new List<Order>();
        return orders;
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        var mongoOrders = await _collection
            .Find(o => o.CustomerId == customerId.ToString())
            .ToListAsync();
        var orders = mongoOrders?
            .Select(mo => mo.MapToDomain())
            .ToList() 
            ?? new List<Order>();
        return orders;
    }

    public async Task<Order?> GetByOrderIdAsync(int orderId)
    {
        var mongoOrder = await _collection
            .Find(o => o.Id == orderId)
            .FirstOrDefaultAsync();
        return mongoOrder?.MapToDomain();
    }
}
