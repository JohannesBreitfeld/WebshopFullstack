using Webshop.Application.DTOs.UserDTOs;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginUserRequest request);
    Task<AuthResponse?> RegisterAsync(RegisterUserRequest request);
}