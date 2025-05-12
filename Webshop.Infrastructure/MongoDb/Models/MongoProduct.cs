using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoProduct
{
    [BsonId]
    public int Id { get; set; } 

    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public int StockBalance { get; set; }
    public int Status { get; set; }  
    public string ImageUrl { get; set; } = string.Empty;

    public int? ProductCategoryId { get; set; }

    public bool SoftDeleted { get; set; }
}
