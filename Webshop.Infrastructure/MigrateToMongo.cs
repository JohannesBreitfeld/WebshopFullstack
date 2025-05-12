using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Webshop.Infrastructure.MongoDb.Mapping;
using Webshop.Infrastructure.MongoDb.Models;
using Webshop.Infrastructure.SQL.Data;

namespace Webshop.Infrastructure;

public class MigrateToMongo
{
    private readonly AppDbContext _sqlContext;
    private readonly IMongoDatabase _mongoDatabase;

    public MigrateToMongo(AppDbContext sqlContext, IMongoDatabase mongoDatabase)
    {
        _sqlContext = sqlContext;
        _mongoDatabase = mongoDatabase;
    }

    public async Task Migrate()
    {
        var mongoProductCollection = _mongoDatabase.GetCollection<MongoProduct>("Products");
        var mongoCustomerCollection = _mongoDatabase.GetCollection<MongoCustomer>("Customers");
        var mongoOrderCollection = _mongoDatabase.GetCollection<MongoOrder>("Orders");
        var mongoProductCategoryCollection = _mongoDatabase.GetCollection<MongoProductCategory>("ProductCategories");

        var sqlProducts = await _sqlContext.Products.ToListAsync();
        var mongoProducts = sqlProducts.Select(p => p.MapToMongo());
        await mongoProductCollection.InsertManyAsync(mongoProducts);

        var sqlCustomers = await _sqlContext.Customers.ToListAsync();
        var mongoCustomers = sqlCustomers.Select(c => c.MapToMongo());
        await mongoCustomerCollection.InsertManyAsync(mongoCustomers);

        var sqlOrders = await _sqlContext.Orders
            .Include(o => o.OrderProducts) 
            .ToListAsync();
        var mongoOrders = sqlOrders.Select(o => o.MapToMongo());
        await mongoOrderCollection.InsertManyAsync(mongoOrders);

        var sqlProductCategories = await _sqlContext.ProductCategories.ToListAsync();
        var mongoProductCategories = sqlProductCategories.Select(pc => pc.MapToMongo());
        await mongoProductCategoryCollection.InsertManyAsync(mongoProductCategories);
    }
}
