using MongoDB.Driver;

namespace Webshop.Infrastructure.MongoDb.SequenceServices;

public class SequenceService
{
    private readonly IMongoCollection<Counter> _collection;

    public SequenceService(IMongoDatabase database)
    {
        _collection = database.GetCollection<Counter>("Counters");
    }

    public async Task<int> GetNextSequenceValueAsync(string sequenceName)
    {
        var filter = Builders<Counter>.Filter.Eq(c => c.Id, sequenceName);
        var update = Builders<Counter>.Update.Inc(c => c.SequenceValue, 1);

        var options = new FindOneAndUpdateOptions<Counter>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        var result = await _collection.FindOneAndUpdateAsync(filter, update, options);
        return result.SequenceValue;
    }
}
