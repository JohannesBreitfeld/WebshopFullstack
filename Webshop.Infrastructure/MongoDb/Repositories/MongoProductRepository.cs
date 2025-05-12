using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;
using Webshop.Infrastructure.MongoDb.SequenceServices;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoProductRepository : IProductRepository
{
    private readonly IMongoCollection<MongoProduct> _collection;
    private readonly SequenceService _sequenceService;

    public MongoProductRepository(IMongoDatabase database, SequenceService sequenceService)
    {
        _collection = database.GetCollection<MongoProduct>("Products");
        _sequenceService = sequenceService;
    }

    public async Task AddAsync(Product product)
    {
        var nextId = await _sequenceService.GetNextSequenceValueAsync("Products");
        product.Id = nextId;
        var mongoProduct = product.MapToMongo();
        await _collection.InsertOneAsync(mongoProduct);
    }

    public void Delete(Product product)
    {
        _collection.DeleteOne(p => p.Id == product.Id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var mongoProducts = await _collection.Find(_ => true).ToListAsync();
        var products = mongoProducts?
            .Where(mp => mp.SoftDeleted == false)
            .Select(mp => mp.MapToDomain())
            .ToList() 
            ?? new List<Product>();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var mongoProduct = await _collection
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
        return mongoProduct?.MapToDomain();
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        var mongoProduct = await _collection
            .Find(p => p.Name == name)
            .FirstOrDefaultAsync();
        return mongoProduct?.MapToDomain();
    }

    public void Update(Product product)
    {
        var mongoProduct = product.MapToMongo();
        _collection.ReplaceOne(p => p.Id == mongoProduct.Id, mongoProduct);
    }
}
