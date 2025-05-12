using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Webshop.Infrastructure.MongoDb.Models;

public class MongoCustomer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string StreetAdress { get; set; } = string.Empty;
    public int PostalCode { get; set; } 
    public string City { get; set; } = string.Empty;

    public List<string> OrderIds { get; set; } = [];
}
