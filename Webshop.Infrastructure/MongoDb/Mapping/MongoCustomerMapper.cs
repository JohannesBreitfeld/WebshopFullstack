using Webshop.Domain.Entities;
using Webshop.Infrastructure.MongoDb.Models;

namespace Webshop.Infrastructure.MongoDb.Mapping;

public static class MongoCustomerMapper
{
    public static Customer MapToDomain(this MongoCustomer mongo)
    {
        return new Customer
        {
            Id = mongo.Id, 
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
            Id = domain.Id,
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            PhoneNumber = domain.PhoneNumber,
            Email = domain.Email,
            StreetAdress = domain.StreetAdress,
            PostalCode = domain.PostalCode,
            City = domain.City
        };
    }
}