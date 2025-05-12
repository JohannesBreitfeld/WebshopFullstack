using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoCustomerMapper
{
    public static Customer MapToDomain(this MongoCustomer mongo)
    {
        return new Customer
        {
            Id = int.Parse(mongo.Id), 
            FirstName = mongo.FirstName,
            LastName = mongo.LastName,
            PhoneNumber = mongo.PhoneNumber,
            Email = mongo.Email,
            StreetAdress = mongo.StreetAdress,
            PostalCode = mongo.PostalCode,
            City = mongo.City,
            Orders = new List<Order>() 
        };
    }

    public static MongoCustomer MapToMongo(this Customer domain)
    {
        return new MongoCustomer
        {
            Id = domain.Id.ToString(),
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            PhoneNumber = domain.PhoneNumber,
            Email = domain.Email,
            StreetAdress = domain.StreetAdress,
            PostalCode = domain.PostalCode,
            City = domain.City,
            OrderIds = domain.Orders.Select(o => o.Id.ToString()).ToList()
        };
    }
}