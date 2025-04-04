using Microsoft.IdentityModel.Abstractions;
using Webshop.Application.DTOs.Requests;
using Webshop.Application.DTOs.UserDTOs;
using Webshop.Presentation.Models;

namespace Webshop.Presentation.Mapping;

public static class UserMapping
{
    public static RegisterUserRequest MapToRequest(this RegisterModel model)
    {
        return new RegisterUserRequest()
        {
            Password = model.Password,
            Customer = new CreateCustomerRequest()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAdress = model.StreetAdress,
                PostalCode = model.PostalCode,
                City = model.City
            }
        };
    }
}
