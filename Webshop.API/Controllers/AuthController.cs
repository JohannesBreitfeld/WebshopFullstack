using Microsoft.AspNetCore.Mvc;
using Webshop.Application.DTOs.UserDTOs;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        return response is not null ? Ok(response) : BadRequest("Registration failed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return response is not null ? Ok(response) : Unauthorized("Invalid credentials");
    }
}
