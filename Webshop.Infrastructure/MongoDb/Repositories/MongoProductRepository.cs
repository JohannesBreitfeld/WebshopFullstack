using MongoDB.Driver;
using Webshop.Domain.Entities;
using Webshop.Domain.Interfaces;

namespace Webshop.Infrastructure.MongoDb.Repositories;

public class MongoProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public MongoProductRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Product>("Products");
    }
    public async Task AddAsync(Product product)
    {
        
        
        await _collection.InsertOneAsync(product);

    }
        
        
        

    public void Delete(Product product) => _collection.DeleteOne(p => p.Id == product.Id);

    public async Task<IEnumerable<Product>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<Product?> GetByNameAsync(string name) => await _collection.Find(p => p.Name == name).FirstOrDefaultAsync();

    public void Update(Product product) => _collection.ReplaceOne(p => p.Id == product.Id, product);
}
