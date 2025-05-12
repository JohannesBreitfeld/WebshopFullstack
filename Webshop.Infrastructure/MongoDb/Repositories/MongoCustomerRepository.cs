using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoCustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<MongoCustomer> _collection;

    public MongoCustomerRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<MongoCustomer>("Customers");
    }

    public async Task AddAsync(Customer customer)
    {
        var mongoCustomer = customer.MapToMongo();
        await _collection.InsertOneAsync(mongoCustomer);
    }

    public async void Delete(Customer customer)
    {
        await _collection.DeleteOneAsync(x => x.Id == customer.Id.ToString());
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
        var mongoCustomer = await _collection.Find(x => x.Id == id.ToString()).FirstOrDefaultAsync();
        return mongoCustomer?.MapToDomain();
    }


    public async void Update(Customer customer)
    {
        var mongoCustomer = customer.MapToMongo();
        await _collection.ReplaceOneAsync(x => x.Id == mongoCustomer.Id, mongoCustomer);
    }
}
