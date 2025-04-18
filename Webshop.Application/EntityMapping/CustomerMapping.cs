using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.Responses;
using Webshop.Domain.Entities;

namespace Webshop.Application.EntityMapping;

public static class CustomerMapping
{
    public static Customer MapToCustomer(this CreateCustomerRequest request)
    {
        return new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            StreetAdress = request.StreetAdress,
            PostalCode = request.PostalCode,
            City = request.City,
            PhoneNumber = request.PhoneNumber
        };
    }

    public static CustomerResponse MapToResponse(this Customer customer)
    {
        return new CustomerResponse()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            StreetAdress = customer.StreetAdress,
            PostalCode = customer.PostalCode,
            City = customer.City,
            PhoneNumber = customer.PhoneNumber
        };
    }

    public static CustomersResponse MapToResponse(this IEnumerable<Customer> customers)
    {
        return new CustomersResponse
        {
            Items = customers.Select(c => c.MapToResponse())
        };
    }

    public static Customer MapToCustomer(this UpdateCustomerRequest request, int id)
    {
        return new Customer()
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            StreetAdress = request.StreetAdress,
            PostalCode = request.PostalCode,
            City = request.City,
            PhoneNumber = request.PhoneNumber
        };
    }
}
