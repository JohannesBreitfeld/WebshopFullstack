using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;
using Webshop.Infrastructure.MongoDb.SequenceServices;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoCustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<MongoCustomer> _collection;
    private readonly SequenceService _sequenceService;

    public MongoCustomerRepository(IMongoDatabase database, SequenceService sequenceService)
    {
        _collection = database.GetCollection<MongoCustomer>("Customers");
        _sequenceService = sequenceService;
    }

    public async Task AddAsync(Customer customer)
    {
        var nextId = await _sequenceService.GetNextSequenceValueAsync("Customers");
        customer.Id = nextId;
        var mongoCustomer = customer.MapToMongo();
        await _collection.InsertOneAsync(mongoCustomer);
    }

    public async void Delete(Customer customer)
    {
        await _collection.DeleteOneAsync(x => x.Id == customer.Id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var mongoCustomers = await _collection.Find(_ => true).ToListAsync();
        var customers = mongoCustomers.Select(m => m.MapToDomain()).ToList();
        return customers;
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        var mongoCustomer = await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        return mongoCustomer?.MapToDomain();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var mongoCustomer = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return mongoCustomer?.MapToDomain();
    }


    public async void Update(Customer customer)
    {
        var mongoCustomer = customer.MapToMongo();
        await _collection.ReplaceOneAsync(x => x.Id == mongoCustomer.Id, mongoCustomer);
    }
}
