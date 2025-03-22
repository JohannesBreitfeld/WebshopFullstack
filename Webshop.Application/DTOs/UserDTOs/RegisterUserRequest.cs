using Webshop.Application.DTOs.Requests;

namespace Webshop.Application.DTOs.UserDTOs;

public class RegisterUserRequest
{
    public string Password { get; set; } = string.Empty;
    public CreateCustomerRequest Customer { get; set; } = new();
}
