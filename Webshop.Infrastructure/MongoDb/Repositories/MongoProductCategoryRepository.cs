using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;
using Webshop.Infrastructure.MongoDb.SequenceServices;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoProductCategoryRepository : IProductCategoryRepository
{
    private readonly IMongoCollection<MongoProductCategory> _collection;
    private readonly SequenceService _sequenceService;

    public MongoProductCategoryRepository(IMongoDatabase database, SequenceService sequenceService)
    {
        _collection = database.GetCollection<MongoProductCategory>("ProductCategories");
        _sequenceService = sequenceService;
    }
    public async Task AddAsync(ProductCategory category)
    {
        var nextId = await _sequenceService.GetNextSequenceValueAsync("ProductCategories");
        category.Id = nextId;
        var mongoProductCategory = category.MapToMongo();
        await _collection.InsertOneAsync(mongoProductCategory);
    }

    public void Delete(ProductCategory category)
    {
        _collection.DeleteOne(c => c.Id == category.Id);
    }

    public async Task<IEnumerable<ProductCategory>> GetAllAsync()
    {
        var mongoProductsCategories = await _collection
            .Find(_ => true)
            .ToListAsync();
        var productsCategories = mongoProductsCategories?
            .Select(mpc => mpc.MapToDomain())
            .ToList() 
            ?? new List<ProductCategory>();
        return productsCategories;
    }

    public async Task<ProductCategory?> GetByIdAsync(int id)
    {
       var mongoProductCategory = await _collection
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();
       return mongoProductCategory?.MapToDomain(); 
    }

    public void Update(ProductCategory category)
    {
        var mongoProductCategory = category.MapToMongo();
        _collection.ReplaceOne(c => c.Id == category.Id, mongoProductCategory);
    }
}
