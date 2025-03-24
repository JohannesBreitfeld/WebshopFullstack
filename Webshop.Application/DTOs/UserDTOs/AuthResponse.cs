using Webshop.Application.DTOs.Responses;

namespace Webshop.Application.DTOs.UserDTOs;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public CustomerResponse? Customer { get; set; }
}
