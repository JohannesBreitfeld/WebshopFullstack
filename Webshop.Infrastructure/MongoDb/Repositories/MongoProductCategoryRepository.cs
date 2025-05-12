using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoProductCategoryRepository : IProductCategoryRepository
{
    private readonly IMongoCollection<ProductCategory> _collection;

    public MongoProductCategoryRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ProductCategory>("ProductCategories");
    }
    public async Task AddAsync(ProductCategory category) => await _collection.InsertOneAsync(category);

    public void Delete(ProductCategory category) => _collection.DeleteOne(c => c.Id == category.Id);

    public async Task<IEnumerable<ProductCategory>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<ProductCategory?> GetByIdAsync(int id) => await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();

    public void Update(ProductCategory category) => _collection.ReplaceOne(c => c.Id == category.Id, category);
}
